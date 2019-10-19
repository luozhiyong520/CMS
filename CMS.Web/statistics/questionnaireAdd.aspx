<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- #include file="/controls/header.htm" -->
</head>
<body>
    <div class="cz_bk">
        <div class="lm_div">
            问卷信息</div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="t_table" id="newsinfo">
            <tr>
                <td style="width: 20%">
                    问卷标题：
                </td>
                <td>
                    <input type="hidden" id="pId" value="" />
                    <input type="text" id="tit" name="tit" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    问卷说明
                </td>
                <td>
                    <textarea id="desc" name="desc" cols="80" rows="5"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <input class="inButton" type="button" value="添 加" name="addWenjuan" id="addWenjuan" />
                </td>
            </tr>
        </table>
    </div>
    <div class="cz_bk wtxx" style="display:none">
        <div class="lm_div">
            问题列表</div>
        <div style="float: right">
            <input class="inButton" type="button" value="添加问题" name="addWenti" id="addWenti" /></div>
  <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="40%" nowrap>标题</TH>
        <TH width="20%" nowrap>类型</TH>
         <TH width="20%" nowrap>操作</TH>
    </TR>
     <tr class="td_ys">
     </tr>
    </table>
    </div>
    <div id="divAddItem" title="问题信息" style="padding: 18px; display: none">
        <table cellpadding="0" border="0px">
            <tr>
                <td style="width: 76px">
                    标题:
                </td>
                <td>
                    <input name="tit" type="text" maxlength="30" id="qTit" class="w300 inTextbox" />
                </td>
            </tr>
            <tr>
                <td>
                    类型:
                </td>
                <td>
                    <select id="qType" name="qType" style="width: 140px;">
                        <option value="单选">单选</option>
                        <option value="多选">多选</option>
                        <option value="文字">文字</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>

    <div id="divAddItem1" title="选项" style="padding: 18px; display: none">
    <table cellpadding="0" border="0px">
        <tr>
            <td style="width:76px">问题:</td>
	        <td><input type="text" maxlength="25" id="qTit1" class="w300 inTextbox" readonly=readonly /></td>
        </tr>
        <tr>
            <td valign="top">选项:</td>
            <td id="Options">
            </td>
        </tr>
        <tr>
            <td></td>
            <td><a style="float:right; padding:10px 0 0 10px;" href="javascript:void(0)" onclick="AddOptionLi('','')"> 添加 </a></td>
        </tr>


    </table>
</div>
    <script type="text/javascript" src="/js/authoritydot.js"></script>
    <script type="text/javascript">
        $(function () {
            //-----问卷按钮请求
            $("#addWenjuan").click(function () {
                if ($("#pId").val()) {
                    $.ajax({
                        type: "POST",
                        url: "/AjaxQuestionnaire/UpdateQuestionnaire.cspx",
                        data: $.param({ qId: $("#pId").val() }) + "&" + $.param({ pId: 0 }) + "&" + $.param({ tit: $("#tit").val() }) + "&" + $.param({ desc: $("#desc").val() }),
                        success: function (resText) {
                        }
                    });
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "/AjaxQuestionnaire/AddQuestionnaire.cspx",
                        data: $.param({ pId: 0 }) + "&" + $.param({ tit: $("#tit").val() }) + "&" + $.param({ desc: $("#desc").val() }),
                        success: function (resText) {
                            if (resText > 0) {
                                window.location = "/statistics/questionnaireEdit.aspx?pid=" + resText;
                            }
                        }
                    });
                }
            })
            //----问卷按钮请求结束

            $("#addWenti").click(function () {
                $("#qTit").val();
                // 显示编辑对话框
                ShowEditItemDialog('test', 'divAddItem', 500, 250, function (j_dialog) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var pId = $("#pId").val();
                    $.ajax({
                        type: "POST",
                        url: "/AjaxQuestionnaire/AddQuestionnaire.cspx",
                        data: $.param({ pId: pId }) + "&" + $.param({ tit: $("#qTit").val() }) + "&" + $.param({ type: $("#qType").val() }),
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function (responseText) {
                            if (responseText > 0) {
                                var link = "<a  href='javascript:void(0)' onclick=EditOptions(" + responseText + ",'" + $("#qTit").val()
                         + "')>编辑选项</a>";
                                var html = " <tr class='td_ys'><td width=40%' align='center'>" + $("#qTit").val() + "</td>"
                         + "<td width=10%' align='center'>" + $("#qType").val() + "</td>"
                         + "<td width=10%' align='center'>"
                                if ($("#qType").val() == "文字") {
                                    html += "</dt></tr>";
                                }
                                else {
                                    html += link + "</dt></tr>";
                                }
                                $(".c_table").append(html);
                            }
                        }
                    });
                });
                return true;
            })
        })

        function EditOptions(qId, tit) {
            $("#qTit1").val(tit);
            $.ajax({
                type: "POST",
                url: "/AjaxQuestionnaire/GetOptions.cspx",
                data: { "qId": qId },
                success: function (resText) {
                    $("#Options").html("");
                    if (resText.length == 0) {
                        $("#Options").html("");
                    } else {
                        for (var i = 0; i < resText.length; i++) {
                            AddOptionLi(res[i].Info, resText[i].Order, res[i].QId, res[i].QoId);
                        }
                    }
                }
            });
            ShowEditItemDialog('test', 'divAddItem1', 500, 250, function (j_dialog) {
                var j_waitDialog = ShowWaitMessageDialog();
                var options = $("#Options").find("li");
                var strOptions = "";
                options.each(function () {
                    if ($(this).find("input:first").val() != "") {
                        strOptions += $(this).find("input:first").val() + "," + $(this).find("input:last").val() + "," + $(this).attr("id") + ";";
                    }
                })
                strOptions = strOptions.substr(0, strOptions.length - 1);
                $.ajax({
                    type: "POST",
                    url: "/AjaxQuestionnaire/UpdateQuestionnaireOptions.cspx",
                    data: $.param({ qId: qId }) + "&" + $.param({ options: strOptions }),
                    complete: function () { HideWaitMessageDialog(j_waitDialog); },
                    success: function (responseText) {
                        $.messager.alert(g_MsgBoxTitle, "成功保存!", "info", function () {
                        });
                    }
                });
            })
        }

        function AddOptionLi(a1, a2, qid, id) {
            var OptionLi = "<li id=\"" + id + "\" qid='" + qid + "'><input type=\"text\" style=\"width:190px;\" class=\"inTextbox\" value=\"" + a1 + "\" /> 排序:<input type=\"text\" style=\"width:60px;\" class=\"inTextbox\" value=\"" + a2 + "\" /><a style=\"float:right; padding:10px 0 0 10px;\" id=\"" + id + "\"  href=\"javascript:void(0)\" onclick=\"DelOption(this,"+id+",'"+a1+"')\"> 删除 </a></li>";
            $("#Options").append(OptionLi);
        }


        function DelOption(o,qoid, info) {
            if (confirm('你确定删除选项 《' + info + '》吗？')) {
                var j_waitDialog = ShowWaitMessageDialog();
                $.ajax({
                    url: "/AjaxQuestionnaire/DeleteQuestionnaireOption.cspx?qoId=" + qoid,
                    type: "GET",
                    complete: function () { HideWaitMessageDialog(j_waitDialog); },
                    success: function (responseText) {
                        $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                            o.parent().remove();
                        });
                    }
                })
            }
        }
    </script>
</body>
</html>
