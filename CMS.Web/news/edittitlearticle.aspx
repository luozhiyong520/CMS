<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑标题文章</title>
      <!-- #include file="/controls/header.htm" -->  
</head>
<body>
     <iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>
     <div class="con_p"> 
        <div class="cz_bk">
 
        <div class="lm_div">编辑标题文章</div>
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="titleinfo">
        <TR>
              <TD width="10%" align="right" nowrap> 信息标题：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
 
                <INPUT name="title" class= "inTextbox" id="title" oninput="getInputNum()" value="<%=Model.News.Title %>"  style="width:500px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" />
                <input type="hidden"  name="titlecolor" value=""  id="titlecolor">
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
                <img border=0 src="/themes/default/images/admin/Gcolor.gif" style="cursor:pointer;background-Color:<%=titlecolor%>" align="absmiddle" onClick="getColor(this,'titlecolor');" />
                &nbsp;是否粗体:<input type="checkbox" name="blod" id="strong" <% if(Model.News.IsBold == "1" ){ %> checked="checked" <% } %> />&nbsp;已输入字数<span id="inputNum" style="color:Red;">0</span>个   <Font color="#FF0000">* 必填内容不允许为空,请输入</Font>
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
              <TD  width="10%" align="right" nowrap> 重要性：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
               
                 <select id="important" class="inTextbox" name="important"  style="width:160px">     
                       <option value="1" <%if(Model.News.Important==1){ %> selected="selected"<%} %>>一星</option>    
                       <option value="2" <%if(Model.News.Important==2){ %> selected="selected"<%} %>>二星</option>  
                       <option value="3" <%if(Model.News.Important==3){ %> selected="selected"<%} %>>三星</option>  
                       <option value="4" <%if(Model.News.Important==4){ %> selected="selected"<%} %>>四星</option>  
                       <option value="5" <%if(Model.News.Important==5){ %> selected="selected"<%} %>>五星</option>       
                  </select>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>


             <TR>
              <TD  width="10%" align="right" nowrap> 图片：</TD>
              <TD width="30%">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
               <input type="radio" name="uploadpic" checked="checked"  id="webpic"/>网上图片 
                <input type="radio" name="uploadpic" id="localpic"/>图片库上传
          
                <br />
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
                     } %>
                  </span>
               </td>
            </TR>

             
            <TR>
              <TD align="right" nowrap>相关信息：</TD>
              <TD colspan="3">
              <div style="float:left;">
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               责任编辑:<INPUT name="CreatedUser" size="12" maxlength="50" class= "inTextbox"  value="<%=Model.News.CreatedUser%>"/>
                </div>
                <div style="position:relative; float:left; width:600px;">
                &nbsp;&nbsp;上市公司: <INPUT id="stockcode" size="60" onblur="setTag()" maxlength="60" class= "inTextbox"  value=""/> 
                <input type="hidden" value="<%=Model.News.StockCode%>" id="stockinfo" />
                 <ul id="taglist" class="tagul">
                   
                 </ul>
                </div>
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
               <INPUT name="CreatedTime" size="20" id="CreatedTime" class= "inTextbox"   value="<%=Model.News.CreatedTime %>"/>&nbsp;&nbsp;
               
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
               <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status" id="truestatus" <% if(Model.News.Status.Value == 1){ %> checked="checked" <%} %> value="1"/> 否:<input value="0" id="falsestatus" type="radio" name="status" /></TD>
            </TR>
               
            </TR>

                <TR>
                <TD align="right" nowrap>摘要：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 
                <textarea style="width:600px; height:100px;"  class= "inTextbox" name="newsabstract" id="newsabstract"><%=Model.News.NewsAbstract %></textarea>
                </TD>
            </TR> 

           
            
            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="4" name="Sort" />
                <input type="hidden" value="<%=Model.News.NewsId%>" name="NewsId" id="newsId" />
                <input type="hidden" value="<%=Model.News.SecondTitle%>" name="SecondTitle" id="SecondTitle" />
                <input type="hidden" value="<%=Model.News.SecondUrl%>" name="SecondUrl" id="SecondUrl" />
                <input type="hidden" value="edit" name="operateType" />
                <INPUT class="inButton" type="button" value="编  辑" name="editTitleArticle" id="editTitleArticle" ></TD>
            </TR>



        </TABLE>
     
    </div>



    </div>
    <style type="text/css">
   .tagul li{ background:#999; color:#fff; float:left; padding:0 4px; margin:5px;}
   .tagul li a{ margin-left:10px; color:#fff;}
</style>
 
     <script type="text/javascript" src="/js/editarticle.js"></script>
     <script type="text/javascript" src="/js/globalarticle.js"></script>
     <script src="/js/promptbox.js" type="text/javascript"></script>
    
     <script type="text/javascript">
         $(function () {

             var v = $("#stockinfo").val();
             var strs = new Array(); //定义一数组
             if (v != "") {
                 strs = v.split(",");
                 for (var i = 0; i < strs.length; i++) {
                     $("#taglist").append("<li><span class=\"tag-btn\">" + strs[i] + "</span><a href=\"javascript:void(0)\">x</a></li>");
                 }
                 $("#taglist a").click(function () {
                     $(this).parent().remove();
                 })
             }
         });
         function setTag() {
             var isinput = true;
             for (var i = 0; i < $("#taglist li").length; i++) {
                 if ($("#stockcode").val() == $("#taglist li").eq(i).children().html()) {
                     isinput = false;
                 }
                 // alert($("#taglist li").eq(i).children().html())   
             }

             if ($("#stockcode").val() != "" && $("#stockcode").val() != "输入名称 / 代码 / 简拼" && isinput == true) {
                 $("#taglist").append("<li><span class=\"tag-btn\">" + $("#stockcode").val() + "</span><a href=\"javascript:void(0)\">x</a></li>");
             }
             $("#stockcode").val("输入名称/代码/简拼");

             $("#taglist a").click(function () {
                 $(this).parent().remove();
             })
         }
 
	
        var promptbox = new promptbox();
        promptbox.bind({
            "inputid": "stockcode",
            "default": "输入名称/代码/简拼",
            "type": "11",
            "max": 10,
            "head": ["选择", "证券代码", "证券名称"],
            "body": [-1, 2, 4],
            "fix": {
                "ie6": [0, -1],
                "ie7": [0, -1],
                "firefox": [1, 1]
            },
            "callback": null
        });
        jQuery(function () {
            jQuery("#stockcodeButton").click(function () {
                var stockcode = jQuery.trim(jQuery("#stockcode").val(), "");
                if (stockcode == "输入名称/代码/简拼") {
                    stockcode = "";
                }
                if (stockcode != "") {
                    if (Common.CheckStockcode(stockcode) == false) return false;
                }
                window.open("http://www.upchina.com/webshangwubu/get.php?stockCode=" + stockcode);
            });
        });

</script>

</body>
</html>
