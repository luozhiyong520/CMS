<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新增个人信息</title>
    <!-- #include file="/controls/header.htm" -->
      <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"/> 
     <link href="/themes/default/styles/upload.css" rel="stylesheet" type="text/css"/>
     
</head>
<body>
    <div class="con_p" style="overflow:hidden; margin-bottom:60px;">
        <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
           <a href="analysttotallist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>分析师列表</a> 
           |<a href="analystadd.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>添加分析师</a>
        </div>
    </div>
        <div class="cz_bk" >
            <table id="tableinfo" cellpadding="0" cellspacing="0" width="95%" style=" height:350px;">
                <tr>
                    <td rowspan="6" style="width: 120px; text-align: center;">
                         <span id="imagesrc"></span>
                    </td>
                    <td style="width: 100px;">
                        名称：
                    </td>
                    <td>
                        <input id="txtAnalystName" name="AnalystName"  value="" class="easyui-validatebox" maxlength="20"
                            style="width: 280px;" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        别名：
                    </td>
                    <td>
                        <input id="NickName" name="NickName"  value="" class="easyui-validatebox" maxlength="20"
                            style="width: 280px;" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        粉丝：
                    </td>
                    <td>
                        <input id="txtFansNum"  name="FansNum" onkeyup="this.value=this.value.replace(/\D/g,'')"
                            onafterpaste="this.value=this.value.replace(/\D/g,'')" class="easyui-validatebox"
                            style="width: 280px;" type="text" />
                            <input type="hidden" id="hdFansNum" />
                    </td>
                </tr>
                <tr>
                    <td>
                        直播类型：
                    </td>
                    <td>
                       <select id="AnalystType" name="AnalystType" style="width:280px;">
                       <option value="1">现货分析师</option>
                        <option value="2">贵金属分析师</option>
                       </select>
                    </td>
                </tr>
                 <tr>
                    <td>
                        VIP分析师类型：
                    </td>
                    <td>
                       <select id="VipType" name="VipType" style="width:280px;">
                       <option value="0">--普通版--</option>
                       <option value="1">白金版</option>
                        <option value="2">钻石版</option>
                        <option value="3">至尊版</option>
                        <option value="4">银利阁</option>
                        <option value="5">淘金殿</option>
                       </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        软件版本：
                    </td>
                    <td>
                       <select id="SoftVersion" name="SoftVersion" style="width:280px;">
                        <option value="0">--默认版--</option>
                       <option value="1">金蝴蝶</option>
                        <option value="5001">金牡丹</option>
                        </select>
                    </td>
                </tr>

                <tr>
                    <td style="width: 120px;  vertical-align:top; text-align: center; height:60px;">
                    <span id="spanButtonImage"></span>
                    </td>
                    <td>
                        公告：
                    </td>
                    <td>
                        <textarea id="txtNotice"  name="Notice" class="easyui-validatebox" maxlength="50"
                            cols="20" rows="2" style="width: 680px; height: 50px;"></textarea>
                             <input type="hidden" id="hdNotice" />
                    </td>
                </tr>
                

                <tr>
                    <td>&nbsp;</td>
                    <td>
                        简介：
                    </td>
                    <td>
                        <%--<textarea id="txtIntro"  name="Intro" class="easyui-validatebox" maxlength="200" cols="20"
                            rows="2" style="width: 680px; height: 80px;"></textarea>--%>
                               <textarea  name="Intro" id="txtIntro"></textarea>
                            <input type="hidden" id="hdIntro" />
                    </td>
                </tr>
                 <tr>
                 <td>&nbsp;</td>
                    <td>
                       雷达测评：
                    </td> 
                    <td>
                     准确： <input type="text" value="" name="Accuracy"/>
                    稳定性：<input type="text" value="" name="Stability"/>
                    防御力：<input type="text" value="" name="Defense"/>
                    攻击力：<input type="text" value="" name="Attack"/>
                    心态：<input type="text" value="" name="Mentality"/>
                    </td>
                </tr>
              
                <tr>
                 <td>&nbsp;</td>
                    <td>
                       状态：
                    </td> 
                    <td>
                        <input id="yes" type="radio" value="1" name="AnalystStatus" checked="checked"/>正常
                    <input id="no" type="radio" value="2" name="AnalystStatus"/>停用
                    </td>
                </tr>
                <tr>
                    <td>
                    &nbsp;
                    </td>
                    <td>
                       <input id="btnEdit" type="button"  style=" width:65px; height:25px; display:none; float:left;" value="修 改" />
                       <input id="btnSave" type="button"  style=" width:65px; height:25px;" value="新 增" />
                    </td>
                    <td>
                        <input id="btnReset" type="reset"  style=" width:65px; height:25px; margin-left:0px; float:left;display:none;" value="取 消" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script src="../themes/default/scripts/swfupload/handler.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/swfupload/swfupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        var swfupic;
        window.onload = function () {
            swfupic = new SWFUpload({
                // Backend Settings
                upload_url: "uploadimg.ashx",
                post_params: {
                    "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                },

                // File Upload Settings
                file_size_limit: "500 K",
                file_types: "*.gif;*.png;*.jpg;*.bmp;*.jpeg;*.jpe",
                //file_types_description : "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                upload_success_handler: uploadSuccess1,
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
               file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
               // upload_complete_handler: uploadComplete,

                // Button settings
                //button_image_url : "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonImage",
                button_width: 80,
                button_height: 28,
                button_text: '<span class="button" style="cursor:pointer;">修改头像</span> ',
                button_text_style: '.button { color:#0000FF; padding-right:20px }',
                button_text_top_padding: 8,
                button_text_left_padding: 5,
                button_cursor: SWFUpload.CURSOR.HAND,

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
        function uploadSuccess1(file, serverData) {
            $("#imagesrc").html('');
            $("#imagesrc").append("<img src=" + serverData + " width=80px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl'/> ");

        };
    </script>

    <script type="text/javascript">

        $(function () {
            self.parent.document.body.style.overflow = 'hidden';
            var editor = new baidu.editor.ui.Editor({
                UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
                autoClearinitialContent: true,
                iframeCssUrl: '/themes/default/scripts/ueditor/themes/default/iframe.css',
                initialContent: '',
                initialFrameWidth: 685,
                initialFrameHeight: 200,
                minFrameHeight: 150,
                toolbars: [
                                ['source', 'fontfamily', 'fontsize', 'bold', 'italic', 'underline', 'forecolor', 'backcolor', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'link', 'insertvideo']
                            ]
            });
            editor.render('txtIntro');

            //新增Analyst
            $("#btnSave").click(function () {

                if (ValidateForm()) {
                    var Intro = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
                    if (Intro.length < 1) {
                        $.messager.alert('消息提示', '简介不能为空。', 'warning');
                    }
                    var str = $("#tableinfo :input").fieldSerialize() + "&Intro=" + Intro;

                    $.ajax({
                        url: "/AjaxAnalyst/AnalystAdd.cspx",
                        data: str,
                        type: "POST",
                        success: function (json) {
                            if (json != null) {
                                if (json == "cz") {
                                    $.messager.alert('消息提示', '分析师名称已经存在！', 'info');
                                } else {
                                    $.messager.alert('消息提示', '新增成功！', 'info', function () {
                                        window.location = window.location;
                                    });
                                }
                            }
                        }
                    });
                }
            });
            //重置
            $("#btnReset").click(function () {
                $("#txtFansNum").val($("#hdFansNum").val());
                $("#txtIntro").val($("#hdIntro").val());
                $("#txtNotice").val($("#hdNotice").val());
            })
        });
//验证方法 
function ValidateForm() {
    if (ValidateControl("#txtAnalystName", "名称 不能为空。") == false) return false;
    if (ValidateControl("#txtFansNum", "粉丝数 不能为空。") == false) return false;
    if (ValidateControl("#AnalystType", "直播类型 不能为空。") == false) return false;
    if ($("#imagesrc input[type='hidden']").val() == null || $("#imagesrc input[type='hidden']").val() == "" || $("#imagesrc input[type='hidden']").val() == undefined) {
        $.messager.alert('消息提示', '上传头像 不能为空。', 'warning');
        return false;
    }
    return true;
}
    </script>
</body>
</html>
