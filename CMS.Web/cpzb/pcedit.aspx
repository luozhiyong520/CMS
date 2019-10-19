<%@ Page Language="C#" Inherits="PageView<AnalystLivePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>直播编辑</title>
     <!-- #include file="/controls/header.htm" --> 
         <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"/>  
     <link href="http://js.upchina.com/sasweb/goodsbillboard/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
      .showpclist{ border:1px solid #ccc; background:#eee;}
      .showpclist li{ width:240px; cursor:pointer; border-bottom:1px solid #ccc}
      .showpclist li span{ margin:0 5px;}
     </style>

   
</head>
<body>
   <div class="con_p"> 

    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
             <a href="zbadd.aspx?AnalystId=<%=Model.AnalystLive.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystLive.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加直播</a> 
           | <a href="zblist.aspx?AnalystId=<%=Model.AnalystLive.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystLive.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>直播列表</a> 
           | <a href="cprecord.aspx?AnalystId=<%=Model.AnalystLive.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystLive.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"   class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>操盘记录</a> 
        <span style="color:Red">你现在操作的分析师：<%=Model.AnalystLive.AnalystName%></span>
        </div>
    </div>

       <div class="cz_bk">
 
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"  bgcolor="#ffffcc">
          <tr><td colspan="5" bgcolor="#555555" style="color:#fff;padding-left:10px">近30天他的战绩</td></tr>
            <TR>
                <TD width="20%" align="center" nowrap>开多</TD>
                <TD width="20%" align="center">开空</TD>
                <TD width="20%" align="center">盈利</TD>
                <TD width="20%" align="center">亏损</TD>
                <TD width="20%" align="center">成功率</TD>
            </TR>
            <TR>
                 <TD align="center" id="kdnum"><%=Model.AnalystLive.BigNum %></TD>
                <TD align="center" id="kknum"><%=Model.AnalystLive.EmptyNum %></TD>
                <TD align="center" id="ylnum"><%=Model.AnalystLive.ProfitNum %></TD>
                <TD align="center" id="ksnum"><%=Model.AnalystLive.LossNum %></TD>
                <TD align="center" id="cgpercent"><%=Model.AnalystLive.SuccessRate %>%</TD>
            </TR>
        </TABLE>
       </div>

        <div class="cz_bk" style="margin-bottom:60px;">
            <TABLE cellSpacing="0" cellPadding="0" border="0" id="zbinfo" style="position:relative" >
             <TR>
              <TD>
                &nbsp;&nbsp;操作:
                <select id="LiveType" name="LiveType" onchange="isDisable(this.options[this.options.selectedIndex].value)">
                  <option value="2" selected="selected" >平仓</option>
                </select> <font color="red"> *必选</font>
               &nbsp;&nbsp;商品名称:<INPUT name="ActualName" id="ActualName" size="30" maxlength="50" class= "inTextbox"  value="<%=Model.AnalystLive.ActualName%>" disabled="disabled"/><%if (Model.AnalystLive.LiveType != 3){ %><font color="red"> *必选<%} %></font>
                   <ul style="display:none; position:absolute; left:190px" id="showpclist" class="showpclist">
                   </ul>
                   
                <INPUT type="hidden" name="ActualCode" id="ActualCode" size="30" maxlength="50" class= "inTextbox"  value="<%=Model.AnalystLive.ActualCode%>"/>
                &nbsp;&nbsp;价格:<INPUT name="TransPrice" id="TransPrice" size="10" maxlength="10" class= "inTextbox"  value=""/><%if (Model.AnalystLive.LiveType != 3){ %><font color="red"> *必填<%} %>
                &nbsp;&nbsp;类型:
                 <select id="TransType" name="TransType">
                    <%if (Model.AnalystLive.TransType == "买入订立"){ %>
                         <option value="卖出转让" id="mczr">卖出转让</option>
                    <%}else if (Model.AnalystLive.TransType == "卖出订立"){ %>
                         <option value="买入转让" id="mrzr">买入转让</option>
                      <%}else if (Model.AnalystLive.TransType == "买入开仓"){ %>
                         <option value="卖出平仓" id="mcpc">卖出平仓</option>
                    <%}else if (Model.AnalystLive.TransType == "卖出开仓")
                      { %>
                         <option value="买入平仓" id="mrpc">买入平仓</option>
                     <%} %>
                   
                </select><%if (Model.AnalystLive.LiveType != 3){ %><font color="red"> *必选<%} %>
                &nbsp;&nbsp;止盈价:<INPUT name="StopProfit" id="StopProfit" size="10" maxlength="10" class= "inTextbox"  value="<%=Model.AnalystLive.StopProfit %>"/>
                &nbsp;&nbsp;止损价:<INPUT name="StopLoss"  id="StopLoss" size="10" maxlength="10" class= "inTextbox"  value="<%=Model.AnalystLive.StopLoss %>"/>
               &nbsp;&nbsp;发布时间:<INPUT name="CreateTime"  id="CreateTime" size="30" maxlength="50" class= "inTextbox"  value="<%=DateTime.Now.ToString()%>"/>
               </TD>
            </TR>

              <tr>
               <TD style="padding:6px 10px;">
                 <textarea  name="info" id="info"></textarea><span style="color:Red;">*注：内容小于2000字,观点不能为空</span>
                 <p><span id="spanButtonImage" style=" font-weight:bold"></span> </p>

                 <p><span id="imagesrc">
                 <%
                     string[] array;
                     if (!string.IsNullOrEmpty(Model.AnalystLive.ImgUrl))
                     {
                         array = Model.AnalystLive.ImgUrl.Split(',');

                         for (int i = 0; i < array.Length; i++)
                         {
                             int hrefLength = array[i].ToString().Length;
                             int line = array[i].LastIndexOf("/") + 1;
                             int line1 = array[i].ToString().LastIndexOf(".");
                             var name = array[i].Substring(line, line1 - line);
                  %>
                       <span id="<%=name%>"> <img src="<%=array[i]%>"  width=60px height=80px /><input type='hidden' value="<%=array[i]%>" name='ImgUrl'/><a href='javascript:void(0)' onclick=deletepic('<%=name%>')> 删除</a> </span>
                 <%}
                     } %>
                 </span>  </p>
               </TD>
               <tr>
               <TD style="padding:6px 10px;">
                 &nbsp;&nbsp;超链接:<INPUT name="LinkUrl" id="LinkUrl" size="100" maxlength="100" class= "inTextbox"  value=""/> <font color=red>请以http://开头</font>
               </TD>

            </tr>

               <tr>
               <TD style="padding:6px 10px;" align="center" >
                  <input type="hidden" name="pc_LiveId" id="pc_LiveId" value="<%=Model.AnalystLive.LiveId%>" />
                  <input type="hidden" name="pc_ActualName" id="pc_ActualName" value="<%=Model.AnalystLive.ActualName%>"/>
                  <input type="hidden" name="pc_ActualCode" id="pc_ActualCode" value="<%=Model.AnalystLive.ActualCode%>"/>
                  <input type="hidden" name="operate" value="add" id="operate" />
                   <input type="hidden" name="AnalystName" value="<%=Model.AnalystLive.AnalystName%>" id="AnalystName" />
                     <input type="hidden" name="AnalystId" value="<%=Model.AnalystLive.AnalystId%>" id="AnalystId" />
                     <input type="hidden" name="AnalystType" value="<%=Model.AnalystType%>" id="AnalystType" />
                  <input type="button" value="提 交" class="inButton" id="Addzbinfo" />
               </TD>
            </tr>
            </TABLE>
        </div>
    </div>
     <script src="http://js.upchina.com/sasweb/goodsbillboard/js/jquery.autocomplete.min.js" type="text/javascript"></script>
    <script src="http://js.upchina.com/sasweb/goodsbillboard/js/topinyin.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function removeHTMLTag(str) {
            str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
            str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
            //str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
            str = str.replace(/&nbsp;/ig, ''); //去掉&nbsp;
            return str;
        }
        $(function () {
            self.parent.document.body.style.overflow = 'hidden';
            var editor = new baidu.editor.ui.Editor({
                UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
                autoClearinitialContent: false,
                iframeCssUrl: '/themes/default/scripts/ueditor/themes/default/iframe.css',
                initialContent: '<%=Model.AnalystLive.Info%>',
                initialFrameWidth: 700,
                initialFrameHeight: 300,
                minFrameHeight: 150,
                toolbars: [
                                     ['source', 'fontfamily', 'fontsize', 'bold', 'italic', 'underline', 'forecolor', 'backcolor', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'link', 'insertvideo']
                                    ]
            });
            editor.render('info');
            //开仓/平仓商品
            $("#Addzbinfo").click(function () {

                if ($("#LiveType").val() == 0) {
                    alert("请选择一个操作类型!");
                    return false;
                } else if ($("#LiveType").val() == 1 || $("#LiveType").val() == 2) {
                    if ($("#TransType").val() == 0) {
                        alert("请选择一个类型操作!");
                        return false;
                    }
                }

                if ($("#LiveType").val() != 3) {
                    if ($("#ActualName").val() == "") {
                        alert("商品名称不能为空!");
                        return false;
                    }
                }

                if (!checknum($("#TransPrice").val()) || $("#TransPrice").val() == "") {
                    alert("价格必须为数字!");
                    return false;
                }
                if ($("#TransPrice").val() != "") {
                    if (!checknum($("#StopProfit").val())) {
                        alert("止盈价必须为数字!");
                        return false;
                    }
                }
                if ($("#TransPrice").val() != "") {
                    if (!checknum($("#StopLoss").val())) {
                        alert("止损价必须为数字!");
                        return false;
                    }
                }

                var info = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
                if (removeHTMLTag(editor.getContent()).length > 2000) {
                    alert("内容超出了2000字!");
                    return false;
                }
                var AnalystId = $("#AnalystId").val();
                var AnalystName = $("#AnalystName").val();
               // var str = decodeURIComponent($("#zbinfo :input").fieldSerialize(), true) + "&Content=" + info;
                var str = $("#zbinfo :input").fieldSerialize()+ "&Content=" + info;
                $("#Addzbinfo").val("正在提交中...");
                $("#Addzbinfo").attr("disabled", true);
                $.ajax({
                    url: "/AjaxAnalystLive/AddEditAnalystLive.cspx",
                    data: str,
                    type: "POST",
                    complete: function () { },
                    success: function (responseText) {
                        if (responseText == "000001") {
                            alert("请选择一个操作类型");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000002") {
                            alert("请选择一个交易类型");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000003") {
                            alert("必须为数字");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000005") {
                            alert("商品名称不能为空");
                            $("#Addzbinfo").attr("disabled", false);
                        }
                        else {
                            alert("操作成功");
                            window.location = 'zbadd.aspx?AnalystId=' + AnalystId + '&AnalystName=' + encodeURI(AnalystName);
                        }

                    }
                })

            });


        });
        //检查是否数字
        function checknum(num) {
            if (!isNaN(num) || isNum(num)) {
                return true;
            } else {
                return false;
            }
        }
        function isNum(s) {
            //var regu = "^([0-9]*)$";
            var regu = "^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$"; // 小数测试
            var re = new RegExp(regu);
            if (s.search(re) != -1)
                return true;
            else
                return false;
        }

 
 
    </script>
  <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css">
    <script src="../themes/default/scripts/swfupload/handler.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/swfupload/swfupload.js" type="text/javascript"></script>
     <script type="text/javascript">

         var swfu, swfupic;
		window.onload = function () {
                swfupic = new SWFUpload({
                    // Backend Settings
                    upload_url: "uploadimg.ashx",
                    post_params: {
                        "ASPSESSID": "<%=CMS.BLL.UserCookies.AdminId %>"
                    },

                    // File Upload Settings
                    file_size_limit: "10 MB",
                    file_types: "*.gif;*.png;*.jpg;*.bmp;*.jpeg;*.jpe",
                    //file_types_description : "JPG Images",
                    file_upload_limit: 0,    // Zero means unlimited

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
                    button_placeholder_id: "spanButtonImage",
                    button_width: 90,
                    button_height: 28,
                    button_text: '<span class="button">点击上传图片</span> ',
                    button_text_style: '.button { color:#ff0000; padding-right:20px;font-weight:bold; }',
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
                var hrefLength = serverData.length;
                var line = serverData.lastIndexOf("/") + 1;
                var line1 = serverData.lastIndexOf(".");
                var name = serverData.substr(line, line1 - line);
                $("#imagesrc").append("<span id=" + name + "><img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl'/><a href='javascript:void(0)' onclick=deletepic('" + name + "')> 删除</a> </span>");
               // $("#imagesrc").append("<span id=" + name + "><img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl'/>");
            };
            function deletepic(picid) {
                $("#" + picid).empty();
            }
  </script>
</body>
</html>
