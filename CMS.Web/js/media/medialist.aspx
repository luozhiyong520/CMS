<%@ Page Language="C#" Inherits="PageView<MediaPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>文件上传</title>    
   <!-- #include file="/controls/header.htm" -->
    <script type="text/javascript" src="../js/medialist.js"></script>
    <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
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
                file_types: "*.gif;*.png;*.jpg;*.bmp;*.jpeg;*.jpe",
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
                button_text_style: '.button { color:#ff0000 }',
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

<div class="con_p">
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a href="medialist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >文件列表</a> | <a class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  href="javascript:void(0)" id="MediaInfo" >添加文件</a>
              | <a name="Button1" id="Button1"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit'  onclick="setChecked();" />全选</a> | <a name="Button1" id="Button2" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit'  onclick="setInverseChecked();"/>反选</a> | <a name="Submit1" id="Submit1"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-cancel' />批量删除<a></a> 
                        </tr></table></td>
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
                     <select id="MediaClass">
                        <option value="0">-请选择类别-</option>
                        <% foreach (MediaClass mediaClass in Model.MediaClassList)
                           { 
                        %>
                        <option value="<%=mediaClass.MediaClassId%>"><%=mediaClass.MediaClassName%></option>
    
                        <%} %>
                    </select>
                     </td>

                    <td>标题：<input class="inTextbox" type="text" name="txtKeyword" id="txtKeyword" maxlength="50" size="25" value=""/></td>
                    <td class="p10">文件日期 从 </td>
                        <td><input type="text" id="txtStartDate" name="StartDate" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td style="width: 35px; text-align: right"> 到 </td>
                        <td><input type="text" id="txtEndDate" name="EndDate" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td><a href="#" id="btnQuery" class="easyui-linkbutton" iconCls="icon-find">查找文件</a></td>
                    </tr>
                    </table></td>

                 
                    </tr>
                </table>
            </div>
        </div>



         <div class="cz_bk">

         <table id="tblQueryResult"></table>
             
       </div>



 



 <div id="MediaAddEdit" title="文件" class="easyui-draggable" data-options="handle:'#title'" style="height:600px; display:none; position:absolute; width:800px;  top:40px; left:200px; border:1px solid #99BBE8;background:transparent url(/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_title.png); padding:5px; "> 
      <div style="height:32px; line-height:24px; font-weight:bold;"  id="title">
         <div style="float:left; padding-left:5px; color:#15428b">添加图片</div>
         <div style="float:right; height:16px; width:20px; margin-top:2px; background:url('/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_tools.gif') no-repeat -16px 0px; cursor:pointer;" id="closepic"></div>
      </div>
      <div style="clear:both"></div>
      <div style="height:560px; overflow:auto; background:#ffffff;">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" class="tdBorder">
    
        <tr align="left">
          <td colspan="3"><strong>设置图片参数及相关信息>>>>>></strong></td>
        </tr>

        <tr class="tdc2">
          <td width="15%" height="27">分类编号</td>
          <td colspan="2" >
            <select id="MediaClassId" class= "inTextbox" name="MediaClassId">
                    <% foreach (MediaClass mediaClass in Model.MediaClassList)
                       { 
                    %>
                    <option value="<%=mediaClass.MediaClassId%>"><%=mediaClass.MediaClassName%></option>
    
                    <%} %>
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
            先根据信息填写所需配置，再点击"请选择图片上传",可以选择一张或多张图片。<br />
            上传的图片文件不得超过：2048K&nbsp;&nbsp; <br />
            支持文件格式：gif,png,jpg,bmp,jpeg,jpe<br />
            注：文件只能上传到子分类中,上传图片，点击"选择本地图片（单图最大为 2048KB，支持多选）"按钮后会自动判断文件大小自动上传，上传成功后可以直接关闭本窗口。</font></td>
        </tr>
      
      </table> 
     </div>
</div>



    </div>




 









</body>
</html>