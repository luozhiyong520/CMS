
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" Inherits="PageView<AnalystLivePageModel>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>直播编辑</title>
<!-- #include file="/controls/header.htm" --> 
<link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"/>  
<script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
<script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
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
            <a href="zbadd.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加直播</a> 
           | <a href="zblist.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>直播列表</a> 
           | <a href="cprecord.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"   class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>操盘记录</a> 
          <span style="color:Red">你现在操作的分析师：<%=Model.AnalystName%></span>
        </div>
    </div>

       <div class="cz_bk">
 
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"  bgcolor="#ffffcc">
          <tr><td colspan="5" bgcolor="#555555" style="color:#fff;padding-left:10px">近30天你的战绩</td></tr>
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
                  <option value="0">选择操作</option>
                  <option value="1">开仓</option>
                  <option value="2">平仓</option>
                  <option value="3">观点</option>
                </select> <font color="red"> *必选</font>
               &nbsp;&nbsp;商品名称:<INPUT name="ActualName" id="ActualName" size="30" maxlength="50" class= "inTextbox"  value=""/><font color="red"  class="bx"> *必选</font>
                   <ul style="display:none; position:absolute; left:190px; z-index:9999" id="showpclist" class="showpclist">
                   </ul>
                <INPUT type="hidden" name="ActualCode" id="ActualCode" size="30" maxlength="50" class= "inTextbox"  value=""/>
                 <INPUT type="hidden" name="LiveId" id="LiveId" size="30" maxlength="50" class= "inTextbox"  value=""/>
                &nbsp;&nbsp;价格:<INPUT name="TransPrice" id="TransPrice" size="10" maxlength="10" class= "inTextbox"  value=""/><font color="red"  class="bx"> *必填</font>
                &nbsp;&nbsp;类型:
                 <select id="TransType" name="TransType">
                  <option value="0">选择类型</option>
                  <%if (Model.AnalystType == 1)
                    { %>
                  <option value="买入订立" id="mrdl">买入订立</option>
                  <option value="卖出订立" id="mcdl">卖出订立</option>
                  <%}
                    else
                    { %>
                  <option value="买入开仓" id="mrkc">买入开仓</option>
                  <option value="卖出开仓" id="mckc">卖出开仓</option>
                  <%} %>
              
                </select><font color="red"  class="bx"> *必选</font>
                &nbsp;&nbsp;止盈价:<INPUT name="StopProfit" id="StopProfit" size="10" maxlength="10" class= "inTextbox"  value=""/>
                &nbsp;&nbsp;止损价:<INPUT name="StopLoss"  id="StopLoss" size="10" maxlength="10" class= "inTextbox"  value=""/>
               &nbsp;&nbsp;发布时间:<INPUT name="CreateTime"  id="CreateTime" size="30" maxlength="50" class= "inTextbox"  value="<%=DateTime.Now.ToString()%>"/>
               </TD>
            </TR>

              <tr>
               <TD style="padding:6px 10px;">
         
                 <textarea  name="info" id="info"></textarea><span style="color:Red;">注：内容小于2000字,观点不能为空</span>
                 <p style=" font-weight:bold"><span id="spanButtonImage"  style=" font-weight:bold"></span></p>
                 <p><span id="imagesrc"></span></p>
               </TD>
               <tr>
               <TD style="padding:6px 10px;">
                 &nbsp;&nbsp;超链接:<INPUT name="LinkUrl" id="LinkUrl" size="100" maxlength="100" class= "inTextbox"  value=""/> <font color=red>请以http://开头</font>
               </TD>

            </tr>

               <tr>
               <TD style="padding:6px 10px;" align="center" >
               <input type="hidden" name="pc_ActualName" id="pc_ActualName" value=""/>
                  <input type="hidden" name="pc_ActualCode" id="pc_ActualCode" value=""/>
                   <input type="hidden" name="pc_LiveId" id="pc_LiveId" value=""/>
                   <input type="hidden" name="operate" value="add" id="operate" />
                    <input type="hidden" name="AnalystName" value="<%=Model.AnalystName%>" id="AnalystName" />
                     <input type="hidden" name="AnalystId" value="<%=Model.AnalystId%>" id="AnalystId" />
                     <input type="hidden" name="AnalystType" value="<%=Model.AnalystType%>" id="AnalystType" />
                  <input type="button" value="发布" class="inButton" id="Addzbinfo" />
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

        //改变事件
        function isDisable(LiveType) {
            $("#StopProfit").val("");
            $("#StopLoss").val("");
            $("#ActualName").val("");
            $("#TransPrice").val("");

            if (LiveType == 1) {
              $("#showpclist").css("display", "none");
                AutoComplete();
              if( <%=Model.AnalystType%>==1 )
              {
                 $("#TransType").html("<option value=\"买入订立\" id=\"mrdl\" selected>买入订立</option><option value=\"卖出订立\" id=\"mcdl\">卖出订立</option>");
              }else
              {
                  $("#TransType").html("<option value=\"买入开仓\" id=\"mrkc\" selected>买入开仓</option><option value=\"卖出开仓\" id=\"mckc\">卖出开仓</option>");
              }

                $(".bx").css("display", "");
                $("#mrdl").css("display", "");
                $("#mcdl").css("display", "");
                $("#zbinfo :input").attr("disabled", false);
               
             
            }
            else if (LiveType == 2) {
               
                $(".ac_results").remove();
                pclist();
              
                $(".bx").css("display", "");
                $("#StopProfit").attr("disabled", true);
                $("#StopLoss").attr("disabled", true);
                $("#ActualName").attr("disabled", true);
                $("#TransPrice").attr("disabled", false);
                $("#TransType").attr("disabled", false);
             
                $("#showpclist li").click(function () {
                    $("#pc_ActualCode").val($(this).find('.pcActualCode').val());
                    $("#pc_LiveId").val($(this).find('.pcLiveId').val());
                    $("#pc_ActualName").val($(this).find('span').html());
                    $("#ActualName").val($(this).find('span').html());
                    $("#showpclist").css("display", "none");
                    //平仓时检查所选商品在开仓时是什么操作类型
                    $.ajax({
                        async: false,
                        url: "/AjaxAnalystLive/IsTransType.cspx?liveId=" + $(this).find('.pcLiveId').val(),
                        type: "get",
                        success: function (res) {
                            if (res == "买入订立") {
                                $("#TransType").html("<option value=\"卖出转让\" id=\"mczr\" selected>卖出转让</option>");
                            } else if(res=="卖出订立") {
                                $("#TransType").html("<option value=\"买入转让\" id=\"mrzr\" selected>买入转让</option>");
                            } else if(res=="买入开仓") {
                                $("#TransType").html("<option value=\"卖出平仓\" id=\"mcpc\" selected>卖出平仓</option>");
                            }else{
                                $("#TransType").html("<option value=\"买入平仓\" id=\"mrpc\" selected>买入平仓</option>");
                            }
                        }
                    });

                })


            } else if (LiveType == 3) {
                $("#ActualName").attr("disabled", true);
                $("#TransPrice").attr("disabled", true);
                $("#TransType").attr("disabled", true);
                $("#StopProfit").attr("disabled", true);
                $("#StopLoss").attr("disabled", true);
                $(".bx").css("display", "none");
          
                $("#showpclist").css("display", "none");

            }
        }
        //获取能够被平仓的商品列表
        function pclist() {
         
            $.ajax({
                cache: false,
                async: false,
                url: "/AjaxAnalystLive/GetProductCode.cspx?LiveType=2&AnalystId="+<%=Model.AnalystId %>,
                type: "get",
                success: function (res) {
                    var result = eval(res);
                    var pclist = "";
                    if (result!= undefined) {
                        for (var i = 0; i < result.length; i++) {
                            pclist += "<li><input type=\"hidden\" value=" + result[i].LiveId + " name=\"pcLiveId\" class=\"pcLiveId\" /><input type=\"hidden\" value=" + result[i].ActualCode + " name=\"pcActualCode\" class=\"pcActualCode\" /> <span>" + result[i].ActualName + " </span> <span>买入价: " + result[i].TransPrice + "</span><span>" + result[i].TransType + "</span>  </li>";
                        }
                        $("#showpclist").html(pclist);
                        $("#showpclist").css("display", "");
                        $(".showpclist li:last").css("border", "none");
                    }
                    
                }
            });


        }

        //        //文本框自动完成功能
        function AutoComplete() {
          
            $.ajax({
                cache: false,
                url: "/AjaxAnalystLive/GetProductCode.cspx?LiveType=1&AnalystType="+<%=Model.AnalystType%>,
                type: "get",
                success: function (res) {
                    var adaptation = FormatAdaptationData(res);
                    AddAdaptation(adaptation);
                }
            });
        }
   
        //        //格式化关联数据, 加上拼音关键字
        function FormatAdaptationData(adaptation, LiveType) {
            adaptation = eval(adaptation);
            for (var i = 0; i < adaptation.length; i++) {
                var pinYin = __pinyin.get(adaptation[i]["FNAME"]);
                adaptation[i]["PinYin"] = pinYin.p + " " + pinYin.l;
            }
            return adaptation;
        }
        //        //绑定自动完成功能, 关联数据
        function AddAdaptation(adaptation, LiveType) {
            $("#ActualName").autocomplete(adaptation, {
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 180,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false,    //自动填充
                formatItem: function (row, i, max) {
                    return "<span style=\"width:70px;padding-right:10px;display:block;float:left;\">[" + row.FCODE + "]</span>" + "<span style=\"display:block;float:left;\">" + row.FNAME + "</span>";
                    return row.FNAME;
                },
                formatMatch: function (row, i, max) {
                    return row.FCODE + row.PinYin + row.FNAME;
                },
                formatResult: function (row) {
                    return row.FNAME;
                }
            }).result(function (event, row, formatted) {
                $("#ActualCode").val(row.FCODE)
            });
        }

        function isActualName(actualname) {
          
            var isexist = false;
            $.ajax({
                cache: false,
                async: false,
                url: "/AjaxAnalystLive/GetProductCode.cspx?LiveType=1&AnalystType="+<%=Model.AnalystType%>,
                type: "get",
                success: function (res) {
                    var st = eval('(' + res + ')');
                    if (st!=undefined) {
                        for (var i = 0; i < st.length; i++) {
                            if ($.trim(st[i].FNAME) == $.trim(actualname)) {
                                isexist = true;
                                $("#ActualCode").val(st[i].FCODE);
                                break;
                            } else {
                                isexist = false;
                            }
                        }
                    }
                }
            });
            return isexist;
        }


        $(function () {
            self.parent.document.body.style.overflow = 'hidden';
            var editor = new baidu.editor.ui.Editor({
                UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
                autoClearinitialContent: true,
                iframeCssUrl: '/themes/default/scripts/ueditor/themes/default/iframe.css',
                initialContent: '',
                initialFrameWidth: 700,
                initialFrameHeight: 300,
                minFrameHeight: 150,
                toolbars: [
                                ['source', 'fontfamily', 'fontsize', 'bold', 'italic', 'underline', 'forecolor', 'backcolor', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'link','insertvideo']
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
                    } else if (isActualName($("#ActualName").val()) == false) {
                        alert("不存在该商品名称!");
                        return false;
                    }
                }

                if ((!checknum($("#TransPrice").val()) || $("#TransPrice").val() == "") && $("#LiveType").val() != 3) {
                    alert("价格必须为数字!");
                    return false;
                }
                if ($("#TransPrice").val() != "") {
                    if (!checknum($("#StopProfit").val()) && $("#LiveType").val() != 3) {
                        alert("止盈价必须为数字!");
                        return false;
                    }
                }
                if ($("#TransPrice").val() != "") {
                    if (!checknum($("#StopLoss").val()) && $("#LiveType").val() != 3) {
                        alert("止损价必须为数字!");
                        return false;
                    }
                }

            
                 var info = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
                if ($("#LiveType").val() == 3) {
                    if (removeHTMLTag(editor.getContent()).length < 1) {
                        alert("大家都需要你的观点内容！");
                        return false;
                    }
                }
                if (removeHTMLTag(editor.getContent()).length > 2000) {
                    alert("内容超出了2000字!");
                    return false;
                }
             
                var str = $("#zbinfo :input").fieldSerialize() + "&Content=" + info;
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
                            $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000002") {
                            alert("请选择一个交易类型");
                              $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000003") {
                            alert("必须为数字!");
                              $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000005") {
                            alert("商品名称不能为空!");
                              $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000006") {
                            alert("大家都需要你的观点内容!");
                              $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else if (responseText == "000007") {
                            alert("观点超出了2000字!");
                              $("#Addzbinfo").val("发布");
                            $("#Addzbinfo").attr("disabled", false);
                        } else {
                            alert("操作成功");
                            window.location = window.location;
                        }

                    }
                })

            });


        });

 
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
               // $("#imagesrc").append("<span id=" + name + "><img src=" + serverData + " width=60px height=80px/><input type='hidden' value=" + serverData + " name='ImgUrl'/> </span>");

            };

         function deletepic(picid) {
             $("#" + picid).empty();
         }
 
  </script>


</body>
</html>
