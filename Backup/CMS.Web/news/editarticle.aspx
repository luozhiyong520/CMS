<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>编辑新文章</title>
     <!-- #include file="/controls/header.htm" -->  
   <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"> 
</head>
<body>
       <iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>
     <div class="con_p"> 
 

        <div class="cz_bk">
 
        <div class="lm_div">编辑新文章</div>
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table" id="newsinfo">

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
                 栏目关联:<input type="hidden" id="hfddlList" value=''  />
                        <input type="text" id="txtlm" name="txtlm" />
                        <input type="hidden" id="lmid" value="" name="lmid" />
                 标题前: <select id="Prefix" class="inTextbox" name="Prefix"  style="width:160px">    
                      <option value=''>-请选择类别-</option>
                     <%=Model.Prefix%>                       
                  </select>
               </TD>
            </TR>

            <TR>
             <TD  width="10%" align="right" nowrap> 文章评级：</TD>
              <TD>&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                可信度:
                 <select id="reliability" class="inTextbox" name="reliability"  style="width:160px">   
                         <option value="0" <% if(Model.News.Reliability == 0){ %> selected="selected" <%} %> >请选择星级</option>  
                        <option value="1" <% if(Model.News.Reliability == 1){ %> selected="selected" <%} %> >半星</option>
                        <option value="2" <% if(Model.News.Reliability == 2){ %> selected="selected" <%} %> >一星</option>        
                        <option value="3" <% if(Model.News.Reliability == 3){ %> selected="selected" <%} %> >一星半</option>  
                        <option value="4" <% if(Model.News.Reliability == 4){ %> selected="selected" <%} %> >二星</option> 
                        <option value="5" <% if(Model.News.Reliability == 5){ %> selected="selected" <%} %> >二星半</option>
                        <option value="6" <% if(Model.News.Reliability == 6){ %> selected="selected" <%} %> >三星</option>   
                        <option value="7" <% if(Model.News.Reliability == 7){ %> selected="selected" <%} %> >三星半</option>  
                        <option value="8" <% if(Model.News.Reliability == 8){ %> selected="selected" <%} %> >四星</option> 
                        <option value="9" <% if(Model.News.Reliability == 9){ %> selected="selected" <%} %> >四星半</option> 
                        <option value="10" <% if(Model.News.Reliability == 10){ %> selected="selected" <%} %> >五星</option>        
                  </select>

                  影响力：
                  <select id="effect" class="inTextbox" name="effect"  style="width:160px">  
                        <option value="0" <% if(Model.News.Effect== 0){ %> selected="selected" <%} %> >请选择星级</option>  
                        <option value="1" <% if(Model.News.Effect== 1){ %> selected="selected" <%} %> >半星</option>
                        <option value="2" <% if(Model.News.Effect == 2){ %> selected="selected" <%} %> >一星</option>        
                        <option value="3" <% if(Model.News.Effect == 3){ %> selected="selected" <%} %> >一星半</option>  
                        <option value="4" <% if(Model.News.Effect == 4){ %> selected="selected" <%} %> >二星</option> 
                        <option value="5" <% if(Model.News.Effect == 5){ %> selected="selected" <%} %> >二星半</option>
                        <option value="6" <% if(Model.News.Effect == 6){ %> selected="selected" <%} %> >三星</option>   
                        <option value="7" <% if(Model.News.Effect == 7){ %> selected="selected" <%} %> >三星半</option>  
                        <option value="8" <% if(Model.News.Effect == 8){ %> selected="selected" <%} %> >四星</option> 
                        <option value="9" <% if(Model.News.Effect == 9){ %> selected="selected" <%} %> >四星半</option> 
                        <option value="10" <% if(Model.News.Effect == 10){ %> selected="selected" <%} %> >五星</option>        
                  </select>
       
               </TD>

            </TR>


            <TR>
              <TD align="right" nowrap>相关摘要：</TD>
              <TD colspan="3">

              &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               来源:<input name="Author" id="Author"  width="150px"  class= "inTextbox"  value="<%=Model.News.Author %>"/>
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               责任编辑:<INPUT name="CreatedUser" size="12" id="CreatedUser" class= "inTextbox"  value="<%=Model.News.CreatedUser %>"/>
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
               <INPUT name="CreatedTime" size="20" id="CreatedTime" class= "inTextbox"   value="<%=Model.News.CreatedTime %>"/>&nbsp;&nbsp;
                   截止时间：
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="DeadLine" size="30" maxlength="50" class= "inTextbox" id="DeadLine"  value="<%=Model.News.DeadLine.ToString()%>"/>&nbsp;&nbsp;
               </TD>
               </TD>
            </TR>

             <TR>
              <TD align="right" nowrap>前值：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="LastValue" size="20" maxlength="50" class= "inTextbox" id="LastValue"  value="<%=Model.News.LastValue%>"/>&nbsp;&nbsp;
 
               </TD>
            </TR>



             <TR>
              <TD align="right" nowrap>预测值：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <textarea rows="6" cols="30" name="ForeCast" id="ForeCast"><%if (!string.IsNullOrEmpty(Model.News.ForeCast)){%><%=Model.News.ForeCast.Replace("|", "\n")%><%} %></textarea>  <font color="#FF0000"><font color="#FF0000">每行输入一个后请回车,格式:中金公司,0.2</font> </font> 
               </TD>
            </TR>

            
            <TR>
              <TD align="right" nowrap>历史分析URL：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="HUrl" size="50" maxlength="50" class= "inTextbox" id="HUrl"  value="<%=Model.News.HUrl%>"/>&nbsp;&nbsp;
               本期策略URL：
               &nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <INPUT name="CUrl" size="80" maxlength="80" class= "inTextbox" id="CUrl"  value="<%=Model.News.CUrl%>"/>&nbsp;&nbsp;
                
               </TD>
            </TR>

            <TR>
              <TD align="right" nowrap>文件上传：</TD>
              <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
            
                  文档上传: 
                  <span style="padding-right:20px"><span id="spanButtonDocument"></span></span>  <span id="documentsrc"><input type='hidden' value="<%=Model.News.FileUrl%>" name='FileUrl'/></span>

                标题图片:<span id="spanButtonImage"></span> <span id="imagesrc"><input type='hidden' value="<%=Model.News.ImgUrl%> " name='ImgUrl'/></span>
                 
               </TD>
            </TR>

              <TR>
                <TD align="right" nowrap>是否通过审核：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status" id="truestatus" <% if(Model.News.Status.Value == 1){ %> checked="checked" <%} %> value="1"/> 否:<input value="0" id="falsestatus" type="radio" name="status" /></TD>
            </TR>
              <TR>
                <TD align="right" nowrap>摘要：</TD>
                <TD colspan="3">&nbsp;&nbsp;<IMG height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                 自动获取文章的前50个字符作为摘要: <input type="radio" name="checkabstract" id="autonews"  value="0" checked="checked"/>
                 手动填写摘要:<input value="1" id="handnews" type="radio" name="checkabstract" />
                 <br />
                 <textarea style="margin-left:10px; margin-bottom:6px; width:500px; height:60px;display:none" class="inTextbox" id="newsabstract"  name="newsabstract"><%=Model.News.NewsAbstract%></textarea>
                 
                 </TD>
            </TR>
            <tr>
               <TD align="right" nowrap>内容：</TD>
               <TD colspan="3" style="padding:6px 10px;">
                 <script type="text/javascript">
                     var editor = new baidu.editor.ui.Editor({
                         UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
                         autoClearinitialContent: false,
                         iframeCssUrl: '/themes/default/scripts/ueditor/themes/default/iframe.css',
                         initialContent: '<%=Model.News.Content %>',
                         minFrameHeight: 150
                     });
                     editor.render('info');
                 </script>
                 <textarea  name="info" id="info" ></textarea>
                 
               </TD>
            </tr>

           
            
            <TR>
                <TD></TD>
                <TD colspan="3" >
                <input type="hidden" value="1" name="Sort" />
                <input type="hidden" value="<%=Model.News.NewsId%>" name="NewsId" id="newsId" />
                <input type="hidden" value="<%=Model.News.SecondTitle%>" name="SecondTitle" id="SecondTitle" />
                <input type="hidden" value="<%=Model.News.SecondUrl%>" name="SecondUrl" id="SecondUrl" />
                <input type="hidden" value="<%=Model.News.Url%>" name="Url" id="Hidden1" />
                <input type="hidden" value="update" name="operateType" />
                <INPUT class="inButton" type="button" value="编 辑" name="editArticle" id="editArticle"  ></TD>
            </TR>

        </TABLE>
     
    </div>

    </div>
     
   
     <script type="text/javascript" src="/js/editarticle.js"></script>
      <script type="text/javascript" src="/js/globalarticle.js"></script>

      
    <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
    <script src="../themes/default/scripts/swfupload/handler.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/swfupload/swfupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        var swfu, swfupic;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "uploaddoc.ashx",
                post_params: {
                    "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                },

                // File Upload Settings
                file_size_limit: "20 MB",
                file_types: "*.doc;*.docx;*.pdf;*.txt;*.RAR;*.XLS",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                //button_image_url : "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonDocument",
                button_width: 80,
                button_height: 28,
                button_text: '<span class="button">点击上传附件</span> ',
                button_text_style: '.button { color:#ff0000; padding-right:20px }',
                button_text_top_padding: 8,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "/themes/default/scripts/swfupload/swfupload.swf", // Relative to this file
                flash9_url: "/themes/default/scripts/swfupload/swfupload_FP9.swf", // Relative to this file

                custom_settings: {
                    upload_target: "documentsrc"
                },

                // Debug Settings
                debug: false
            });

            swfupic = new SWFUpload({
                // Backend Settings
                upload_url: "uploadimg.ashx",
                post_params: {
                    "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                },

                // File Upload Settings
                file_size_limit: "20 MB",
                file_types: "*.gif;*.png;*.jpg;*.bmp;*.jpeg;*.jpe",
                //file_types_description : "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess1,
                upload_complete_handler: uploadComplete,

                // Button settings
                //button_image_url : "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonImage",
                button_width: 80,
                button_height: 28,
                button_text: '<span class="button">点击上传图片</span> ',
                button_text_style: '.button { color:#ff0000; padding-right:20px }',
                button_text_top_padding: 8,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "../themes/default/scripts/swfupload/swfupload.swf", // Relative to this file
                flash9_url: "../themes/default/scripts/swfupload/swfupload_FP9.swf", // Relative to this file

                custom_settings: {
                    upload_target: "imagesrc"
                },

                // Debug Settings
                debug: false
            });
        }

        //上传成功后执行,将img标签的src设置为返回的图片保存好的路径
        function uploadSuccess(file, serverData) {
            var filesrc = $("#documentsrc>input:first").attr("value");
            $("#documentsrc").html("<input type='hidden' value=" + serverData + " name='FileUrl'/>");
            $("#documentsrc").append("<span onclick='delFile()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
            $("#editArticle").attr("disabled", false)
        };
        //上传中执行在hander.js63行中
        function uploadProgress(file, bytesLoaded) {
            try {
                var percent = Math.ceil((bytesLoaded / file.size) * 100);

                var progress = new FileProgress(file, this.customSettings.upload_target);
                progress.setProgress(percent);
                if (percent === 100) {
                    progress.setStatus("Creating thumbnail...");
                    progress.toggleCancel(false, this);
                } else {
                    progress.setStatus("Uploading...");
                    progress.toggleCancel(true, this);
                }
            } catch (ex) {
                this.debug(ex);
            }
            $("#editArticle").attr("disabled", true)
        }
        //上传成功后执行,将img标签的src设置为返回的图片保存好的路径
        function uploadSuccess1(file, serverData) {
            var imgsrc = $("#imagesrc>input:first").attr("value");
            $("#imagesrc").html("<img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl'/> ");
            $("#imagesrc").append("<span onclick='delImg()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
            $("#editArticle").attr("disabled", false)
        };
        function delImg() {
            $("#imagesrc>span").remove();
            $("#imagesrc>img").remove();
            var stats = swfu.getStats();
            stats.successful_uploads--;
            swfu.setStats(stats);
        }
        function delFile() {
            $("#documentsrc>span").remove();
            var stats = swfu.getStats();
            stats.successful_uploads--;
            swfu.setStats(stats);
        }

        (function ($) {
            $.fn.extend({
                MultDropList: function (options) {
                    var op = $.extend({ wraperClass: "wraper", width: "150px", height: "350px", data: "", selected: "" }, options);
                    return this.each(function () {
                        var $this = $(this); //指向TextBox
                        var $hf = $(this).next(); //指向隐藏控件存
                        var conSelector = "#" + $this.attr("id") + ",#" + $hf.attr("id");
                        var $wraper = $(conSelector).wrapAll("<span><span></span></span>").parent().parent().addClass(op.wraperClass);

                        var $list = $('<span class="otherChanneList"></span>').appendTo($wraper);
                        $list.css({ "width": op.width, "height": op.height });
                        //控制弹出页面的显示与隐藏
                        $this.click(function (e) {
                            $(".otherChanneList").hide();
                            $list.toggle();
                            e.stopPropagation();
                        });

                        $(document).click(function () {
                            $list.hide();
                        });

                        $list.filter("*").click(function (e) {
                            e.stopPropagation();
                        });
                        //加载默认数据
                        $list.append('<ul><li><input type="checkbox" class="selectAll" value="" /><span>全部</span></li></ul>');
                        var $ul = $list.find("ul");

                        //加载json数据
                        var listArr = op.data.split("|");
                        var jsonData;
                        for (var i = 0; i < listArr.length; i++) {
                            jsonData = eval("(" + listArr[i] + ")");
                            $ul.append('<li><input type="checkbox" value="' + jsonData.k + '" /><span>' + jsonData.v + '</span></li>');
                        }

                        //加载勾选项
                        var seledArr;
                        if (op.selected.length > 0) {
                            seledArr = selected.split(",");
                        }
                        else {
                            seledArr = $hf.val().split(",");
                        }

                        $.each(seledArr, function (index) {
                            $("li input[value='" + seledArr[index] + "']", $ul).attr("checked", "checked");

                            var vArr = new Array();
                            $("input[class!='selectAll']:checked", $ul).each(function (index) {
                                vArr[index] = $(this).next().text();
                            });
                            $this.val(vArr.join(","));
                        });
                        //全部选择或全不选
                        $("li:first input", $ul).click(function () {
                            if ($(this).attr("checked")) {
                                $("li input", $ul).attr("checked", "checked");
                            }
                            else {
                                $("li input", $ul).removeAttr("checked");
                            }
                        });
                        //点击其它复选框时，更新隐藏控件值,文本框的值
                        $("input", $ul).click(function () {
                            var kArr = new Array();
                            var vArr = new Array();
                            $("input[class!='selectAll']:checked", $ul).each(function (index) {
                                kArr[index] = $(this).val();
                                vArr[index] = $(this).next().text();
                            });
                            $hf.val(kArr.join(","));
                            $this.val(vArr.join(","));
                        });
                    });
                }
            });
        })(jQuery);
        $(document).ready(function () {
            $.ajax({
                url: '/AjaxArticle/GetLmRelated.cspx?newsId=' + $.getUrlVar('newsId'),
                success: function (shtml) {
                    $("#lmid").val(shtml);
                }
            });
            $.ajax({
                url: '/AjaxChannel/GetChannelNodeNews.cspx?typeID=1',
                success: function (shtml) {
                    $("#txtlm").MultDropList({ data: shtml });
                }
            });


        });
	</script>


</body>
</html>
