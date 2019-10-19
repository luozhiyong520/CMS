<%@ Page Language="C#" Inherits="PageView<MediaPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>文件上传</title>   
     <!-- #include file="/controls/header.htm" -->
    <script type="text/javascript" src="../js/medialist.js"></script>
    <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
    <link href="../themes/default/styles/manage.css" type="text/css" rel="stylesheet"/>
    <script src="../themes/default/scripts/swfupload/handlers.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/swfupload/swfupload.js" type="text/javascript"></script>

    <script type="text/javascript">
        var swfu;
        window.onload = function () {
            swfu = new SWFUpload({
                // Backend Settings
                //修改执行上传操作的文件(aspx或ashx)
                upload_url: "upload.ashx",
                post_params: {
                    "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                },

                // File Upload Settings
                file_size_limit: "2 MB",
                file_types: "*.jpg;*.gif;*.png;*.bmp;*.jpe;*.jpeg",
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
                //指定图片上传成功后执行的方法为我们自己定义的ShowData
                upload_success_handler: ShowData,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "../themes/default/scripts/swfupload/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">点此处选择图片上传</span> ',
                button_text_style: '.buttonSmall { color:#ff0000 }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,

                // Flash Settings
                //在这里修改flash插件的引用路径
                flash_url: "../themes/default/scripts/swfupload/swfupload.swf", // Relative to this file
                flash9_url: "../themes/default/scripts/swfupload/swfupload_FP9.swf", // Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });
        }
        //上传成功后执行,将img标签的src设置为返回的图片保存好的路径
        function ShowData(file, serverData) {
            $("#imgSrc").append("<img src=" + serverData + ">");
        };
	</script>


</head>
<body>



    
 <div id="MediaAddEdit" title="文件">

    <table width="100%" border="0" cellpadding="4" cellspacing="1" class="tdBorder">
    
        <tr align="left">
          <td colspan="3"><strong>设置图片参数及相关信息>>>>>></strong></td>
        </tr>

        <tr class="tdc2">
          <td width="15%" height="27">分类编号</td>
          <td colspan="2" >
            <select id="MediaClassId" class= "inTextbox" name="MediaClassId">
                    <% 
                        if (Model.MediaClassList != null)
                        {
                            foreach (MediaClass mediaClass in Model.MediaClassList)
                            { 
                    %>
                    <option value="<%=mediaClass.MediaClassId%>"><%=mediaClass.MediaClassName%></option>
    
                    <%}
                        } %>
                </select>
          </td>
        </tr>

        <tr class="tdc1">
          <td width="15%">小图比例</td>
          <td  colspan="2">
              <select name="MediaScale" id="MediaScale">
                  <option value="h">高</option>
                  <option value="w">宽</option>
                </select>
                <input type="text" name="smallPiexl" id="smallPiexl" size="4" maxlength="3" value="150">
            选择高度或者宽度，按比例生成小图 </td>
        </tr>

        <tr class="tdc2">
          <td width="15%">增加水印 </td>
          <td colspan="2"><table border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td><select name="WaterMark" id="WaterMark" onChange="showAndHiddenSet(this.options[this.selectedIndex].value)">
                    <option value="1">是</option>
                    <option value="0" selected>否</option>
                  </select></td>
              </tr>
            </table></td>
        </tr>

        <tr id="waterImgId" class="tdc1 water" height="65px" style="display:none;">
          <td width="300px" width="15%">水印图片</td>
          <td colspan="2">
          <input type="hidden" id="WaterImagePath" name="WaterImagePath" value="/themes/default/images/white1.png">
          <img id="showImg" src="/themes/default/images/white1.png" alt="" border="0"></td>
        </tr>

        <tr id="waterPosId" class="tdc1 water"  style="display:none;">
          <td width="15%">水印位置 </td>
          <td colspan="2"><select name="waterPosition" id="waterPosition">
              <option value="1">左上</option>
              <option value="2">中上</option>
              <option value="3">右上</option>
              <option value="4">左中</option>
              <option selected value="9">右下</option>
            </select></td>
        </tr>


        <tr class="tdc1">
          <td width="15%">图片标签</td>
          <td colspan="2"> 
              <input type="text" name="MediaLabel" id="MediaLabel" size="18" maxlength="100" class="inTextbox"></td>
        </tr>
        <tr class="tdc2">
          <td width="15%">图片说明</td>
          <td colspan="2"><input type="text" name="MediaDescript" id="MediaDescript" size="35" maxlength="200" class="inTextbox"></td>
        </tr>
        <tr align="left" class="tdc1">
          <td colspan="3"><strong>请先选择文件所属分类，上传文件请输入标题，点击"浏览"按钮（单图最大为 2048KB）"按钮</strong></td>
        </tr>
        
       
        
        <tr class="tdc2" >
          <td width="15%" height="27">标题</td>
          <td colspan="2"><input type="text" id="MediaTitle" name="MediaTitle" size="25" value="picTitle" maxlength="50" class="inTextbox" >
           </td>
      
        </tr>

        <tr class="tdc1">
          <td width="15%">文件</td>
          <td nowrap="nowrap" colspan="2">
              <div id="content">	
	            <div id="swfu_container" style="margin: 0px 10px;">
		            <div>
				        <span id="spanButtonPlaceholder"></span>
		            </div>
	            </div>
		    </div>
           
          </td>
           
        </tr>
 
         <tr align="left" class="tdc2">
         <td width="15%">上传状态</td>
          <td colspan="2">
            <div id="divFileProgressContainer"></div>
             <div id="imgSrc"></div>
          </td>
        </tr>
      
       
       
        <tr class="tdc1">
          <td colspan="3"><font color="red">
            点击"请选择图片上传",选择一张图片。<br />
            上传的图片文件不得超过：2048K&nbsp;&nbsp; <br />
            支持文件格式：gif,png,jpg,bmp,jpeg,jpe<br />
            注：文件只能上传到子分类中,上传图片，点击"选择本地图片（单图最大为 2048KB，支持多选）"按钮后会自动判断文件大小自动上传，如需插入到录入页面编辑器中，请点击"插入图片到编辑框中"按钮 </font></td>
        </tr>
      
      </table> 
     
</div>

</body>
</html>