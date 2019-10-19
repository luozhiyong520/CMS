<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


<head>
    <title>编辑图片文章</title>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"> 
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/editarticle.js"></script>
    <script type="text/javascript" src="/js/globalarticle.js"></script>
</head>
<body>
<iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>
     <div class="con_p"> 
 


        <div class="cz_bk">
 
        <div class="lm_div">编辑图片文章</div>
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="picnews">
          <TBODY>
            <TR>
              <TD width="10%" align="right" nowrap> 信息标题：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="title" class= "inTextbox" id="title" oninput="getInputNum()" value="<%=Model.News.Title %>"  style="width:500px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" />
                <input type="hidden"  name="titlecolor" id="titlecolor">
                 <%
                    string titlecolor="";
                    if(string.IsNullOrEmpty(Model.News.TitleColor))
                    {
                        titlecolor="#fff";
                    }else
                    {
                        titlecolor = Regex.Split(Model.News.TitleColor, @"\[WL\]")[0];
                    }
                   
                     %>
                <img border=0 src="/themes/default/images/admin/Gcolor.gif"  style="cursor:pointer;background-Color:<%=titlecolor%>" align="absmiddle" onClick="Getcolor(this,'title','titlecolor');" />
                &nbsp;是否粗体:<input type="checkbox" name="blod"  id="strong" <% if(Model.News.IsBold == "1" ){ %> checked="checked" <% } %> />&nbsp;已输入字数<span id="inputNum" style="color:Red;">0</span>个   <Font color="#FF0000">* 必填内容不允许为空,请输入</Font>
               </TD>
            </TR>

            <TR>
              <TD width="10%" align="right" nowrap> 信息链接：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="url" size="100" maxlength="400" class= "inTextbox" id="url" value="<%=Model.News.Url %>" /> <Font color="#FF0000">*请以http://开头</Font>
                
               </TD>
            </TR>

            <TR>
              <TD  width="10%" align="right" nowrap> 所属栏目：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <SELECT id="ChannelId" name="ChannelId" style="width:160px">
                    <option value='0'>-请选择类别-</option>
                    <%=Model.Channels %>
                </SELECT>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>

             <TR>
              <TD  width="10%" align="right" nowrap> 图片：</TD>
              <TD width="30%">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
               <input type="radio" name="uploadpic" checked="checked"  id="webpic"/>网上图片 
                <input type="radio" name="uploadpic" id="localpic"/>图片库上传
                <Font color="#FF0000">*</Font>
                <br />
                <%--<input type="text" id="webimg" name="webimg" class="inTextbox" value="<%=Model.News.ImgUrl %>" style=" margin:0 0px 4px 10px; width:300px;"/>
                <input type="text" id="localimg" name="localimg" class="inTextbox"  value="<%=Model.News.ImgUrl %>" style="display:none;margin:0 0 4px 10px;width:300px;" />--%>
                 <input type="text" value="<%=Model.News.ImgUrl%>" id="ImgUrl" name="ImgUrl" class="inTextbox" style="margin:0 0 4px 10px;width:380px;" />
                
               </TD>
               <td  colspan="2">
                  <span style="margin-left:20px; margin-top:5px; float:left">缩略图:
                 <%
                     if (Model.News.ImgUrl != null)
                     {
                         if (Model.News.ImgUrl.Length > 7)
                         {
                             if (Model.News.ImgUrl.Substring(0, 7).ToLower() == "http://")
                             {
                  %>
                 <a href="<%=Model.News.ImgUrl%>" target="_blank"><img src="<%=Model.News.ImgUrl%>"  width="80px" height="80px"/></a>
                 <%}
                             else
                             { %>
                  <a href="http://img.upchina.com<%=Model.News.ImgUrl%>" target="_blank"><img src="http://img.upchina.com<%=Model.News.ImgUrl%>" width="80px" height="80px"/></a>
                  <%}
                         }
                         else
                         {
                      %>
                        <a href="http://img.upchina.com<%=Model.News.ImgUrl%>" target="_blank"><img src="http://img.upchina.com<%=Model.News.ImgUrl%>" width="80px" height="80px"/></a>
                        <%}
                     }%>
                  </span>
               </td>
            </TR>
         
            <TR>
              <TD align="right" nowrap>相关摘要：</TD>
              <TD colspan="3">

              &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               来源:<INPUT name="author" width="150px" id="author" class= "inTextbox" value="<%=Model.News.Author %>"/>
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               责任编辑:<INPUT name="CreatedUser" size="12" id="CreatedUser" class= "inTextbox"   value="<%=Model.News.CreatedUser %>"/>
                &nbsp;&nbsp;关键字:<INPUT name="KeyWord" size="80" id="KeyWord" class= "inTextbox" value="<%=Model.News.KeyWord %>"/>
                <Font color="#FF0000">(多个","分隔或者空格分隔)</Font> 

               </TD>
            </TR>

            <TR>
              <TD align="right" nowrap>权重：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="OrderNum" size="12" id="OrderNum" class= "inTextbox"  value="<%=Model.News.OrderNum %>"/>&nbsp;&nbsp;只能填写60--100之间的数字，数字越大，前台页面排位越靠前
               </TD>
            </TR>
            <TR>
              <TD align="right" nowrap>发布时间：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="createdTime" size="20" id="createdTime" class= "inTextbox"  value="<%=Model.News.CreatedTime %>"/>&nbsp;&nbsp;
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status"  id="truestatus" <% if(Model.News.Status.Value == 1){ %> checked="checked" <%} %> value="1" />
                否:<input id="falsestatus"  type="radio" name="status" <% if(Model.News.Status.Value == 0){ %> checked="checked" <%} %> value="0" /></TD>
            </TR>
              <TR>
                <TD align="right" nowrap>摘要：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 
                <textarea style="width:600px; height:100px;"  class= "inTextbox" name="newsabstract" id="newsabstract"><%=Model.News.NewsAbstract%></textarea>
                </TD>
            </TR>
             
             
            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="3" name="Sort" />
                <input type="hidden" value="<%=Model.News.NewsId%>" name="newsId" id="newsId" />
                 <input type="hidden" value="<%=Model.News.SecondTitle%>" name="SecondTitle" id="SecondTitle" />
                <input type="hidden" value="<%=Model.News.SecondUrl%>" name="SecondUrl" id="SecondUrl" />
                <input type="hidden" value="edit" name="operateType" />
                <INPUT class="inButton" type="button" value="修 改" name="editPicArticle" id="editPicArticle" ></TD>
            </TR>


          </TBODY>
        </TABLE>
     
    </div>



    </div>
</body>
</html>
