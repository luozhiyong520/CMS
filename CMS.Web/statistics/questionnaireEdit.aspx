<%@ Page Language="C#" Inherits="PageView<QuestionnairePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

            <!-- #include file="/controls/header.htm" -->  
                    <script type="text/javascript" src="/js/jquery.dragsort-0.4.min.js"></script>
                <style type="text/css">    
        .cpOrder{display:none;margin: 40px;}
        .cpOrder li{font-size: 14px;padding: 4px 10px; font-weight: bold;color: #E05600;}
        .cpOrder div{padding: 30px 0 0 40px;}
    </style>
</head>
<body>

<div class="con_p" style="padding-bottom:100px;">
<div class="cz_bk" style="display:none" id="Ques">
        <div class="lm_div" >
            问卷信息</div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="t_table" id="newsinfo">
            <tr>
                <td style="width: 20%">
                    问卷标题：
                </td>
                <td>
                    <input type="hidden" id="pId" value="<%=Model.QId %>" />
                    <input type="text" id="tit" name="tit" style="width: 250px" value="<%=Model.Tit %>" />
                </td>
            </tr>
            <tr>
                <td>
                    问卷说明
                </td>
                <td>
                    <textarea id="desc" name="desc" cols="80" rows="5"><%=Model.Desc %></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <input class="inButton" type="button" value="保 存" name="addWenjuan" id="UpdateWenjuan" />
                </td>
            </tr>
        </table>
        </div>
        <div class="cz_bk" style="overflow: hidden;*zoom: 1;">
    <div class="lm_div">管理选项</div>
    <div class="cz_xx" style="float:left;">
           <a href="javascript:void(0)" onclick="AddWenti()" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加问题</a>
    </div>
    <div class="cz_xx" style="float:left;">
        <a href="javascript:void(0)" onclick="SetProgramOrder()" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >问题排序</a>
    </div>
        <div class="cz_xx" style="float:left;">
        <a href="javascript:void(0)" onclick="SetQuestionnaire()" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >编辑问卷</a>
    </div>
        <div id="cpOrder" class="cpOrder"></div>
</div>
<div class="cz_bk">
  <div class="lm_div">问卷列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="40%" nowrap>问卷标题</TH>
        <TH width="20%" nowrap>类型</TH>
         <TH width="20%" nowrap>操作</TH>
    </TR>
    <%foreach (Questionnaires obj in Model.Questions)
      { %>
       <tr class="td_ys">
        <td width="10%" align=center><%=obj.Title %></td>
        <td width="10%" align=center><%=obj.OptType %></td>
        <td width="10%" align=center>
        <a href="javascript:void(0)" onclick="EditTitle(<%=obj.QId %>,'<%=obj.Title %>','<%=obj.OptType %>')">修改标题</a>
        <%if (obj.OptType != "文字")
          { %>
         <a  href="javascript:void(0)" onclick="EditOptions(<%=obj.QId %>,'<%=obj.Title %>')">编辑选项</a>
         <%} %>
         <a href="javascript:void(0)" onclick="DelTitle(<%=obj.QId %>,this)">删除问题</a></td>
       </tr>
      <%} %>
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


<div id="divAddItem2" title="修改标题" style="padding: 18px; display: none">
        <table cellpadding="0" border="0px">
            <tr>
                <td style="width: 76px">
                    标题:
                </td>
                <td>
                    <input name="tit" type="text" maxlength="30" id="qTit2" class="w300 inTextbox" />
                </td>
            </tr> 
        </table>
    </div>

<script type="text/javascript">
    $(function () {
        //-----问卷按钮请求
        $("#UpdateWenjuan").click(function () {
            var desc = $("#desc").val();
            $.ajax({
                type: "POST",
                url: "/AjaxQuestionnaire/UpdateQuestionnaire.cspx",
                data: $.param({ qId: $("#pId").val() }) + "&" + $.param({ pId: 0 }) + "&" + $.param({ tit: $("#tit").val() }) + "&" + $.param({ desc: desc }),
                success: function (resText) {
                    if (resText > 0) {
                        alert("ok");
                    }
                }
            });
        })
        //----问卷按钮请求结束
    })

    function AddWenti() {
        $("#qTit").val("");
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
                        var link = " <a  href='javascript:void(0)' onclick=EditOptions(" + responseText + ",'" + $("#qTit").val()
                         + "')>编辑选项</a>";
                        var html = " <tr class='td_ys'><td width=40%' align='center'>" + $("#qTit").val() + "</td>"
                         + "<td width=10%' align='center'>" + $("#qType").val() + "</td>"
                         + "<td width=10%' align='center'>"
                         + "<a href='javascript:void(0)'"
                         + 'onclick="EditTitle(' + responseText + ",'" + $("#qTit").val() + "','" + $("#qType").val() + "')\">修改标题</a>";
                        if ($("#qType").val() != "文字") html += link
                        html += " <a href='javascript:void(0)' onclick='DelTitle(" + responseText + ",this)'>删除问题</a></dt></tr>"
                        $(".c_table").append(html);
                    }
                    j_dialog.dialog('close');
                }
            });
        });
        return true;
    }

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
                        AddOptionLi(resText[i].Info, '', resText[i].QId,resText[i].QoId);
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
                        j_dialog.dialog('close');
                    });
                }
            });
        })
    }

    function AddOptionLi(a1, a2, qid, id) {
        var OptionLi = "<li id=\"" + id + "\" qid='" + qid + "'><input type=\"text\" style=\"width:190px;\" class=\"inTextbox\" value=\"" + a1 + "\" /> <a style=\"float:right; padding:10px 0 0 10px;\" id=\"" + id + "\"  href=\"javascript:void(0)\" onclick=\"DelOption(this," + id + ",'" + a1 + "')\"> 删除 </a></li>";
        $("#Options").append(OptionLi);
    }


    function DelOption(o, qoid, info) {
        if (confirm('你确定删除选项 《' + info + '》吗？')) {
            var j_waitDialog = ShowWaitMessageDialog();
            $.ajax({
                url: "/AjaxQuestionnaire/DeleteQuestionnaireOption.cspx?qoId=" + qoid,
                type: "GET",
                complete: function () { HideWaitMessageDialog(j_waitDialog); },
                success: function (responseText) {
                    $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                        $(o).parent().remove();
                    });
                }
            })
        }
    }

    function EditTitle(qId, tit, typ) {
        $("#qTit2").val(tit);
        ShowEditItemDialog('test2', 'divAddItem2', 500, 250, function (j_dialog) {
            var j_waitDialog = ShowWaitMessageDialog();

            $.ajax({
                type: "POST",
                url: "/AjaxQuestionnaire/UpdateQuestionnaire.cspx",
                data: $.param({ qId: qId }) + "&" + $.param({ pId: $("#pId").val() }) + "&" + $.param({ tit: $("#qTit2").val() }) + "&" + $.param({ type: typ }),
                complete: function () { HideWaitMessageDialog(j_waitDialog); },
                success: function (responseText) {
                    $.messager.alert(g_MsgBoxTitle, "成功保存!", "info", function () {
                        window.location = window.location;
                    });
                }
            });
        })
    }

    function DelTitle(qId,o) {
        if (confirm('你确定删除问题吗？')) {
            var j_waitDialog = ShowWaitMessageDialog();
            $.ajax({
                url: "/AjaxQuestionnaire/DeleteQuestionnaire.cspx?qId=" + qId,
                type: "GET",
                complete: function () { HideWaitMessageDialog(j_waitDialog); },
                success: function (responseText) {
                    $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                        $(o).parent().parent().remove();
                    });
                }
            })

        }
    }

    function SetQuestionnaire() {
        $("#Ques").is(":hidden") ? $("#Ques").show() : $("#Ques").hide();
    }

    //题目排序
    function SetProgramOrder() {
        $("#cpOrder").is(":hidden") ? GetProgramOrder() : $("#cpOrder").slideUp(500);
    }



    //获取获取排序
    function GetProgramOrder() {
        $.ajax({
            url: "/AjaxQuestionnaire/GetQuestions.cspx",
            data: { pId: $("#pId").val() },
            type: "get",
            success: function (data) {
                $("#cpOrder").html(FormatHtmlProgramOrder(eval(data)));
                $("#cpOrder ul").dragsort();
                $("#cpOrder").slideDown(500);
            }
        });
    }

    function FormatHtmlProgramOrder(data) {
        var res = "";
        for (var i = 0; i < data.length; i++) {
            res += "<li qId='"+data[i].QId+"'>" + data[i].Title + "</li>"
        }
        if (res == "")
            return "<span style=\"color: #C4C4BF;\">没东西</span>";
        return "<ul>" + res + "</ul><div><input id=\"submitCpOrder\" onclick=\"submitCpOrder()\" type=\"button\" value=\"确 定\" style=\"width:80px;\" title=\"提 交\" />&nbsp;&nbsp;&nbsp;&nbsp;<span>提示: 点击拖动</span></div>";
    }

    //提交排序变更
    function submitCpOrder() {
        $("#submitCpOrder").val("提交中...");
        var qo = "";
        $("#cpOrder li").each(function () {
            qo += ((qo == "" ? "" : ",") + $(this).attr("qId") + "|" + $(this).attr("data-itemidx"));
        });
        $.ajax({
            url: "/AjaxQuestionnaire/UpdateQuestionsOrder.cspx",
            data: {pId:$("#pId").val(), qo: qo },
            type: "post",
            success: function (res) {
                $("#submitCpOrder").val("提交成功");
                window.location = window.location;

            }
        });
    }
    </script>
  </div>
</body>
</html>
