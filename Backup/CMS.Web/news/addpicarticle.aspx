<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


<head>
    <title>添加图片文章</title>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
<script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
<link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"> 
     <!-- #include file="/controls/header.htm" -->  
      <script type="text/javascript" src="/js/addarticle.js"></script>
       <script type="text/javascript" src="/js/globalarticle.js"></script>
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
<iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>
     <div class="con_p"> 
 


        <div class="cz_bk">
 
        <div class="lm_div">添加图片文章</div>
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="picnews">
          <TBODY>
            <TR>
              <TD width="10%" align="right" nowrap> 信息标题：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="title" class= "inTextbox" id="title" oninput="getInputNum()" style="width:500px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" />
                <input type="hidden"  name="titlecolor" value=""  id="titlecolor">
                <img border=0 src="/themes/default/images/admin/Gcolor.gif" style="cursor:pointer;background-Color:#fff;" align="absmiddle" onClick="getColor(this,'titlecolor');" />
                &nbsp;是否粗体:<input type="checkbox" name="blod" id="strong"/>&nbsp;已输入字数<span id="inputNum" style="color:Red;">0</span>个   <Font color="#FF0000">* 必填内容不允许为空,请输入</Font>
               </TD>
            </TR>

            <TR>
              <TD width="10%" align="right" nowrap> 信息链接：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="url" size="100" maxlength="400" class= "inTextbox" id="url" /> 
                 <Font color="#FF0000">*请以http://开头</Font>
               </TD>
            </TR>

            <TR>
              <TD  width="10%" align="right" nowrap> 所属栏目：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <select id="ChannelId" class="inTextbox" name="ChannelId"  style="width:160px">                            
                        </select>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>

             <TR>
              <TD  width="10%" align="right" nowrap> 图片：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <input type="radio" name="uploadpic" checked="checked"  id="webpic"/>网上图片 
                <input type="radio" name="uploadpic" id="localpic"/>图片库上传
                <Font color="#FF0000">*</Font>
                <br />
               <%-- <input type="text" value="" id="webimg" name="webimg" class="inTextbox" style=" margin:0 0px 4px 10px; width:260px;"/>--%>
                <input type="text" value="" id="ImgUrl" name="ImgUrl" class="inTextbox" style="margin:0 0 4px 10px;width:380px;" />
               </TD>
            </TR>
         
            <TR>
              <TD align="right" nowrap>相关摘要：</TD>
              <TD colspan="3">

              &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               来源:<INPUT name="Author"  width="150px"  class= "inTextbox"  value=""/>
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               责任编辑:<INPUT name="CreatedUser" size="12" maxlength="50" class= "inTextbox"  value="<%=CMS.BLL.UserCookies.AdminName%>"/>
                &nbsp;&nbsp;关键字:<INPUT name="KeyWord" size="80" maxlength="100" class= "inTextbox"  value=""/>
                <Font color="#FF0000">(多个","分隔或者空格分隔)</Font> 

               </TD>
            </TR>

            <TR>
              <TD align="right" nowrap>权重：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="OrderNum" size="12" maxlength="50" class= "inTextbox"  value="60"/>&nbsp;&nbsp;只能填写60--100之间的数字，数字越大，前台页面排位越靠前
               </TD>
            </TR>
            <TR>
              <TD align="right" nowrap>发布时间：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="createdTime" size="20" id="createdTime" class= "inTextbox"  value="<%=DateTime.Now.ToString()%>"/>&nbsp;&nbsp;
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status"  value="1" checked="checked"/>否:<input value="0"  type="radio" name="status" /></TD>
            </TR>
              <TR>
                <TD align="right" nowrap>摘要：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 
                <textarea style="width:600px; height:100px;"  class= "inTextbox" name="newsabstract" id="newsabstract"></textarea>
                </TD>
            </TR>
             

            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="3" name="Sort" />
                <input type="hidden" value="add" name="operateType" />
                <INPUT class="inButton" type="button" value="添  加" name="addpicnews" id="addpicnews" ></TD>
            </TR>


          </TBODY>
        </TABLE>
     
    </div>



    </div>
</body>
</html>
