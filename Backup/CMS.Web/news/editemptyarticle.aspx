<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


<head>
    <title>编辑空文章</title>
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
 
        <input id="hiddenTitle" type="hidden" />
        <input id="hiddentUrl" type="hidden" />
        <input id="hiddenColor" type="hidden"/>
        <input id="hiddenJC" type="hidden"/>
    
         
        <div class="cz_bk">
 
        <div class="lm_div">添加空文章</div>
         

        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="emptynews"> 
           
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

                        <% 
                        string[] arraytitle = Regex.Split(Model.News.SecondTitle,@"\[WL\]");
                        string[] arrayurl = Regex.Split(Model.News.SecondUrl, @"\[WL\]");
                        string[] arrblod = Regex.Split(Model.News.IsBold, @"\[WL\]");
                        if (Model.News.TitleColor == null)
                        {
                            Model.News.TitleColor = "#333333";
                        }
                        string[] arraycolor = Regex.Split(Model.News.TitleColor, @"\[WL\]");
                        for(int i = 0; i < arraytitle.Length; i++)
                        {
                        %>

                        <tr bgcolor="#FFFFFF" align="center" id="<%=i%>" style="background: none repeat scroll 0% 0% rgb(255, 255, 255);">
                        <td><input type="text" style="width:400px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" value="<%=arraytitle[i]%>" class="inTextbox" name="title"></td>
                        <td><input type="text" class="inTextbox" style="width:260px;" value="<%=arrayurl[i] %>" name="url" size="10">
                        <input type="hidden" name="color" value="<%=arraycolor[i]%>" id="txtColorValue1"></td>
                        <td><img width="18" height="17" border="0" align="middle" style="cursor: pointer; background-color:<%=arraycolor[i]%>" onclick="getColor(this,'txtColorValue1')" alt="标题颜色选取" id="imgColor1" src="/themes/default/images/admin/Gcolor.gif"></td>
                        <td><input type="checkbox" name="JC" <%if (arrblod[i]=="1"){%> checked="checked" <% } %>></td>
                        <td id="line1"><input type="button" onclick="delline(<%=i%>)" class="inTextbox" value="删除"></td>
                        </tr>
                        <%} %>

                    </table>
                </td>
            </tr>

           

            <tr bgcolor="#ffffff">
                <td colspan="5" style="text-align: left">
                    <input id="btnAddTitle" type="button" value="增加一条" class="inTextbox" onclick="addline()" />
                </td>
            </tr>


             
            <TR>
              <TD  width="10%" align="right" nowrap> 所属栏目：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <SELECT id="ChannelId" name="ChannelId" style="width:160px">
                <option value='0'>-请选择类别-</option>
                    <%=Model.Channels%>
                </SELECT>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>

         

            <TR>
              <TD align="right" nowrap>权重：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="OrderNum"  size="12" id="OrderNum" class= "inTextbox"  value="<%=Model.News.OrderNum%>"/>&nbsp;&nbsp;只能填写60--100之间的数字，数字越大，前台页面排位越靠前
               </TD>
            </TR>

             <tr>
              <td align="right" nowrap>责任编辑：</td>
              <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
              <input name="CreatedUser" size="12" maxlength="50" class= "inTextbox"  value="<%=Model.News.CreatedUser%>"/>
               </td>
            </tr>

            <TR>
              <TD align="right" nowrap>发布时间：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="CreatedTime" size="20" class= "inTextbox" id="CreatedTime"  value="<%=Model.News.CreatedTime%>"/>&nbsp;&nbsp;
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status" id="truestatus"  value="1" <%if(Model.News.Status==1){ %> checked="checked"<%} %>/>否:<input value="0" id="falsestatus" type="radio" name="status" <%if(Model.News.Status==0){ %> checked="checked"<%} %>/></TD>
            </TR>
              <TR>
                <TD align="right" nowrap>摘要：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                 <textarea style="margin-left:10px; margin-bottom:6px; width:500px; height:60px;" class="inTextbox" id="newsabstract" name="newsabstract"><%=Model.News.NewsAbstract%></textarea></TD>
            </TR>
             

           
            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="2" name="Sort" />
                <input type="hidden" value="<%=Model.News.NewsId%>" name="newsId" id="newsId" />
                 <input type="hidden" value="edit" name="operateType" />
                <INPUT class="inButton" type="button" value="修 改" name="editEmptyArticle" id="editEmptyArticle"  ></TD>
            </TR>


 
        </TABLE>
     
    </div>



    </div>
</body>
</html>
