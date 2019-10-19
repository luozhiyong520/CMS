<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>专题标题</title>
    <!-- #include file="/controls/header.htm" -->
    <script src="../js/topic.js?11" type="text/javascript"></script>
</head>
<body>
    <div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="addtopic">添加专题</a> | <a  href="javascript:void(0)" id="addtopictype" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加专题类型</a>
            </div>
        </div>
        <div class="cz_bk">
               <div class="lm_div">搜索信息</div>
                 <div class="cz_xx" id="actualsassociateinfo">
                    <span>专题名称：</span><input type="text" id="txtserachTopicName"  maxlength="50" />
                     <span>专题类型：</span>
                     <select id="drpserachTopicTypeName" style=" width:150px;">            
                     </select>
                     <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
                  </div>
            </div>
         <table id="pgtopic"></table>
        </div>

     <%--<div id="divbody" title="专题标题" style="padding: 8px; display:none">--%>
     <div id="divbody" title="文件" class="easyui-draggable" data-options="handle:'#title'" style="height:450px; display:none; position:absolute; width:800px;  top:40px; left:200px; border:1px solid #99BBE8;background:transparent url(/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_title.png); padding:5px;z-index:999; "> 
     <div style="height:32px; line-height:24px; font-weight:bold;"  id="title">
         <div style="float:left; padding-left:5px; color:#15428b">添加专题</div>
         <div style="float:right; height:16px; width:20px; margin-top:2px; background:url('/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_tools.gif') no-repeat -16px 0px; cursor:pointer;" id="closepic"></div>
      </div>
      <div style="clear:both"></div>
      <div style="height:420px; overflow:auto; background:#ffffff;">

        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 130px">
                    专题名称:
                </td>
                <td>
                    <input name="TopicName" type="text" maxlength="50" id="txtTopicName" style="width:200px;" />
                </td>
                 <td style="width: 130px">
                    专题类型名称:
                </td>
                <td>
                    <select id="drpTopicTypeName" style=" width:130px;" style="width:210px;">
                      </select>
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    备注:
                </td>
                <td colspan="3">
                    <textarea id="txtRemark" cols="20" rows="2" maxlength="500" style=" width:560px; height:130px;" name="Remark" class="w300"></textarea>
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    是否置顶:
                </td>
                <td colspan="3">
                    <input type="radio" value="0" name="IsTop" checked="checked" id="IsTopfalse" />否 <input type="radio" value="1"  name="IsTop" id="IsTopTrue"/>是
                </td>
            </tr>

            <tr>
                <td style="width: 130px">
                    创建时间:
                </td>
                <td colspan="3">
                    <input name="CreatedTime" type="text" maxlength="50" id="CreatedTime" style="width:150px;" class="inTextbox"  value="<%=DateTime.Now.ToString()%>" />
                </td>
            </tr>

            <TR>
              <TD align="right" nowrap></TD>
              <TD colspan="3"> 
               <div style="position:relative; margin-left:10px">
                文档上传: <span style="padding-right:20px;"><span id="spanButtonDocument"></span><span id="documentsrc"></span></span>  
                标题图片:<span style=""><span id="spanButtonImage"></span> <span id="imagesrc"></span></span>
                </div>
               </TD>
            </TR>
              <tr>
              <td colspan="4" align=center style="height:80px;">
                <input type="hidden" id="topicId" value=""/>
                <input type="hidden" id="createdUser" value=""/>
                <input type="hidden" id="Status" value=""/>
                <input type="hidden" id="PublishTime" value=""/>
                <input type="button" id="btn_save" value="保 存"  style="display:none;" class="inButton"/>
                <input type="button" id="btn_editsave" value="保 存" style="display:none" class="inButton" />
                <input type="button" value="取 消" id="btn_cancel" class="inButton"/>
            </td>
           
            </tr>
        </table>
    </div>
    <div id="divbodytype" title="专题类型" style="padding: 8px; display:none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 130px">
                    专题类型名称:
                </td>
                <td>
                     <input name="TopicTypeName" type="text" maxlength="50" id="txttypeTopicTypeName" class="w300" />
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    专题生成类型:
                </td>
                <td>
                   <select id="drpTopicTypeGenerate" style=" width:310px;">
                    <option value="">--请选择--</option>
                    <option value="最新">最新</option>
                     <option value="每日">每日</option>
                      <option value="每周">每周</option>
                       <option value="每月">每月</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    模版名称:
                </td>
                <td>
                    <select id="drptypeTemplateId" style=" width:310px;">
                    </select>
                </td>
            </tr>
            
        </table>
    </div>
   
    </div>
     <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
    <script src="../themes/default/scripts/swfupload/handler.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/swfupload/swfupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        var swfu, swfupic;
        window.onload = function () {
            swfu = new SWFUpload({
                upload_url: "uploaddoc.ashx?ChannelId=000001",
                post_params: {
                    "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                },

                file_size_limit: "20 MB",
                file_types: "*.doc;*.docx;*.pdf;*.txt;*.RAR;*.XLS",
                file_types_description: "JPG Images",
                file_upload_limit: 0,

                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,

                //button_image_url : "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonDocument",
                button_width: 80,
                button_height: 28,
                button_text: '<span class="button">点击上传附件</span> ',
                button_text_style: '.button { color:#ff0000; padding-right:20px }',
                button_text_top_padding: 8,
                button_text_left_padding: 5,

                flash_url: "../themes/default/scripts/swfupload/swfupload.swf", // Relative to this file
                flash9_url: "../themes/default/scripts/swfupload/swfupload_FP9.swf", // Relative to this file

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
            $("#documentsrc").html("<input type='hidden' value=" + serverData + " name='FileUrl' id='FileUrl'/>");
            $("#documentsrc").append("<span onclick='delFile()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
           // $("#addArticle").attr("disabled", false)
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
      
        }
        //上传成功后执行,将img标签的src设置为返回的图片保存好的路径
        function uploadSuccess1(file, serverData) {
            var imgsrc = $("#imagesrc img").length;
            if (imgsrc == 0) {
                $("#imagesrc").append("<img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl' id='ImgUrl'/> ");
            }
            else {
                $("#imagesrc").html("<img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl' id='ImgUrl'/>");
             
            }
            $("#imagesrc").append("<span onclick='delImg()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
    
        };

        function delImg() {
            $("#imagesrc>span").remove();
            $("#imagesrc>img").remove();
            $("#imagesrc>input").remove();
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
	</script>

</body>
</html>
