<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>添加标题文章</title>
      <!-- #include file="/controls/header.htm" -->  
</head>
<body>
     <iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>
     <div class="con_p"> 
        <div class="cz_bk">
 
        <div class="lm_div">添加标题文章</div>
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="titleinfo">

            <TR>
              <TD width="10%" align="right" nowrap> 信息标题：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                <INPUT name="title" class= "inTextbox"   oninput="getInputNum()" id="title" style="width:500px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" BorderStyle="Groove" />
              <input type="hidden"  name="TitleColor"  id="titlecolor">
                <img border=0 src="/themes/default/images/admin/Gcolor.gif" style="cursor:pointer;background-Color:#fff;" align="absmiddle" onClick="getColor(this,'titlecolor');" />
                &nbsp;是否粗体:<input type="checkbox" name="blod"  id="strong" />&nbsp;已输入字数<span id="inputNum" style="color:Red;">0</span>个 <Font color="#FF0000">* 必填内容不允许为空,请输入</Font>

           

               </TD>
            </TR>

            <TR>
              <TD  width="10%" align="right" nowrap> 所属栏目：</TD>
              <TD>&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
               
                 <select id="ChannelId" class="inTextbox" name="ChannelId"  style="width:160px">                            
                  </select>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>

            <TR>
              <TD  width="10%" align="right" nowrap> 重要性：</TD>
              <TD>&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
               
                 <select id="important" class="inTextbox" name="important"  style="width:160px">     
                       <option value="1">一星</option>    
                       <option value="2">二星</option>  
                       <option value="3">三星</option>  
                       <option value="4">四星</option>  
                       <option value="5">五星</option>       
                  </select>
                <Font color="#FF0000">*</Font>
               </TD>
            </TR>

             <TR>
              <TD  width="10%" align="right" nowrap> 图片：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <input type="radio" name="uploadpic" checked="checked"  id="webpic"/>网上图片 
                <input type="radio" name="uploadpic" id="localpic"/>图片库上传
               
                <br />
                <input type="text" value="" id="ImgUrl" name="ImgUrl" class="inTextbox" style="margin:0 0 4px 10px;width:380px;" />
               </TD>
            </TR>

            <TR>
              <TD align="right" nowrap>相关信息：</TD>
              <TD colspan="3">
              <div style="float:left;">
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               责任编辑:<INPUT name="CreatedUser" size="12" maxlength="50" class= "inTextbox"  value="<%=CMS.BLL.UserCookies.AdminName%>"/>
                </div>
                <div style="position:relative; float:left; width:600px;">
                &nbsp;&nbsp;上市公司: <INPUT id="stockcode" size="60" onblur="setTag()" maxlength="60" class= "inTextbox"  value=""/> 
                 <ul id="taglist" class="tagul"></ul>
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
               <INPUT name="CreatedTime" size="20" maxlength="50" class= "inTextbox" id="CreatedTime"  value="<%=DateTime.Now.ToString()%>"/>&nbsp;&nbsp;
               </TD>
            </TR>
              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status" value="1" checked="checked"/> 否:<input value="0" type="radio" name="status" /></TD>
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
                <input type="hidden" value="4" name="Sort" />
                <input type="hidden" value="add" name="operateType" />
                <INPUT class="inButton" type="button" value="添  加" name="addArticle" id="addTitleArticle" ></TD>
            </TR>



        </TABLE>
     
    </div>



    </div>

<style type="text/css">
   .tagul li{ background:#999; color:#fff; float:left; padding:0 4px; margin:5px;}
   .tagul li a{ margin-left:10px; color:#fff;}
</style>

 
     <script type="text/javascript" src="/js/addarticle.js"></script>
     <script type="text/javascript" src="/js/globalarticle.js"></script>
     <script src="/js/promptbox.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
         $(document).ready(function () {
             $.ajax({
                 url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
                 success: function (shtml) {
                     $("#ChannelId").html('');
                     $("#ChannelId").append("<option value='0'>-请选择类别-</option>" + shtml);
                 }
             });
             $.ajax({
                 url: '/AjaxTitlePrefix/GetNewsTitlePrefix.cspx',
                 success: function (shtml) {
                     $("#Prefix").html('');
                     $("#Prefix").append("<option value='0'>-请选择类别-</option>" + shtml);
                 }
             });
         })

         function setTag() {
             var isinput=true;
             for (var i = 0; i < $("#taglist li").length; i++) {
                 if ($("#stockcode").val() == $("#taglist li").eq(i).children().html()) {
                     isinput = false;
                 }
                // alert($("#taglist li").eq(i).children().html())   
             }

             if ($("#stockcode").val() != "" && $("#stockcode").val() != "输入名称 / 代码 / 简拼" && isinput==true) {
                 $("#taglist").append("<li><span class=\"tag-btn\">" + $("#stockcode").val() + "</span><a href=\"javascript:void(0)\">x</a></li>");
             }
             $("#stockcode").val("输入名称/代码/简拼");
     
             $("#taglist a").click(function () {
                 $(this).parent().remove();
             })
         }

        
     </script>


     <script type="text/javascript">
          


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
