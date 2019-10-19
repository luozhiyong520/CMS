var Version = "";   //当前版本

$(function () {
    LoadVersionList();
})

//加载课程计划
function LoadCourseProgram() {
    $.ajax({
        url: "/CourseProgram/GetCourseProgram.aspx",
        data: { version: Version },
        type: "get",
        success: function (data) {
            $("#cpList").html(FormatHtml(eval(data)));
            LoadCourseRecommend();
            AddDblclick();
        }
    });
}

function FormatHtml(data) {
    var resHtml = "";
    var res = "";
    var i = 0;  //数据索引
    for (var week = 0; week < 7; week++) {  //固定循环一周
        res = "";
        for (var k = 0; k < 1; k--) {   //死循 数据循环
            if (i < data.length) {      //未超数据索引
                if (week == data[i].Weeks) {    //当前星期数
                    res += FormatHtmlTr(data, i);
                    i++;
                } else {    //非当前星期数    跳出数据循环
                    res = FormatHtmlTop(res, week);
                    break;
                }
            } else {    //超出数据索引即当前星期循环后的星期数没数据   跳出数据循环
                res = FormatHtmlTop(res, week);
                break;
            }
        }
        resHtml += res; //添加当前星期数Html
    }
    return resHtml;
}

function FormatHtmlTr(data, i) {
    return "<tr cpId=\"" + data[i].Id + "\" class=\"td_ys\">" +
                "<td width=\"5%\" align=\"center\"><a edit=\"edit\" title=\"编辑\" href=\"javascript:void(0)\" onclick=\"Edit(this)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/icons/pencil.png\"></a></td>" +
                "<td mt=\"CourseType\" width=\"15%\" align=\"center\">" + data[i].CourseType + "</td>" +
                "<td mt=\"ProgramTimeStart\" width=\"5%\" align=\"center\">" + data[i].ProgramTimeStart + "</td>" +
                "<td mt=\"ProgramTimeEnd\" width=\"5%\" align=\"center\">" + data[i].ProgramTimeEnd + "</td>" +
                "<td mt=\"ProgramName\" width=\"45%\" align=\"left\" title=\"双击编辑\">" + data[i].ProgramName + "</td>" +
                "<td mt=\"Property\" width=\"10%\" align=\"center\">" + data[i].Property + "</td>" +
                "<td mt=\"ProgramUrl\" width=\"10%\" align=\"center\" PassageType=\"" + data[i].PassageType + "\" " + (data[i].PassageType == "1" ? "style=\"color: #0074CC;\"" : "") + ">" + (data[i].ProgramUrl == null || data[i].ProgramUrl == "null" ? "" : data[i].ProgramUrl) + "</td>" +
                "<td width=\"5%\" style=\"border-right: 1px solid #ccc;\" align=\"center\"><a  title=\"删除\" href=\"javascript:void(0)\" onclick=\"DelCp(this)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/images/admin/Item.Doc.Del.gif\"></a></td>" +
            "</tr>";
}
    
function FormatHtmlTop(res, week) {
    if (res == "")
        res = "<tr rest=\"rest\"><th colspan=\"8\">休 息</th></tr>";
    res = "<div week=\"" + week + "\" style=\"padding: 50px 20px 0 30px; font-size: 12px;\">" + //width: 650px;float: left;
                "<h2 style=\"color:#0399CA; padding: 5px 0;\">" + FormatWeek(week) + "</h2>" +
                "<table cellpadding=\"0\" cellspacing=\"0\" class=\"c_table\">" +
                "<tr>" +
                    "<th width=\"5%\" nowrap>&nbsp;</th>" +
                    "<th width=\"15%\" nowrap>课程类型</th>" +
                    "<th width=\"5%\" nowrap>开 始</th>" +
                    "<th width=\"5%\" nowrap>结 束</th>" +
                    "<th width=\"45%\" nowrap>节目名称</th>" +
                    "<th width=\"10%\" nowrap>属 性</th>" +
                    "<th width=\"10%\" nowrap>房 号</th>" +
                    "<th width=\"5%\" style=\"border-right: 1px solid #ccc;\" nowrap>&nbsp;</th>" +
                "</tr>" +
                res +
                "<tr cpid=\"add\" class=\"td_ys\">" +
                    "<td width=\"5%\" align=\"center\" title=\"新增\"><a edit=\"edit\" href=\"javascript:void(0)\" onclick=\"Edit(this)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/icons/edit_add.png\"></a></td>" +
                    "<td></td><td></td><td></td><td title=\"双击新增\"></td><td></td><td></td><td style=\"border-right: 1px solid #ccc;\"></td>" + 
                "</tr>" +
            "</table></div>";
    return res;
}

function FormatWeek(i) {
    var weekDay = ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    return weekDay[i];
}

//删除
function DelCp(o) {
    var thisTr = $(o).parents("tr");
    var cpId = thisTr.attr("cpid");
    if (confirm("确认要删除？")) {
        if ($("#eidtId").val() == cpId) {
            alert("此项编辑中");
            return false;
        }
        $.ajax({
            url: "/CourseProgram/DelCourseProgram.aspx",
            data: { cpId: cpId, version: Version },
            type: "get",
            success: function (res) {
                if (res == "1") {
                    var thisTable = $(o).parents("table");
                    if ((thisTable.find("[cpid]").length) == 2)
                        thisTr.replaceWith("<tr rest=\"rest\"><th colspan=\"8\">休 息</th></tr>");
                    else
                        thisTr.replaceWith("");
                }
                else
                    alert("删除失败!");
            }
        });
    }
}

//添加/编辑
function Edit(obj) {
    RemoveBackgroundColor();
    var thisTr = $(obj).parents("tr");
    var cpId = thisTr.attr("cpId");
    if (thisTr.attr("cpId") == "add") {

    } else {
        if (cpId != "" && cpId != undefined) {
            thisTr.attr("style", "background-color: #ADDFF0");
        }
    }

    OpenEdit(obj);
    $("#subCp").click(function () {
        //$(this).unbind("click");
        if ($(this).val() == "提交中...")
            return false;
        $(this).val("提交中...")
        EditSubmit();
    });
}

//提交变更
function EditSubmit() {

    var cpId = $("#eidtId").val();
    var week = $("#eidtWeek").val();
    var Id = $("#eidtId").val();
    var CourseType = $("#CourseType").val();
    var ProgramTimeStart = $("#ProgramTimeStart").val();
    var ProgramTimeEnd = $("#ProgramTimeEnd").val();
    var ProgramName = $("#ProgramName").val();
    var Property = $("#Property").val();
    var ProgramUrl = $("#ProgramUrl").val();
    var PassageType = $("#PassageType").attr("checked") == "checked" ? "1" : "0";
    var isCheckOk = true;

    if (CourseType == "" || ProgramTimeStart == "" || ProgramTimeEnd == "" || ProgramName == "" || Property == "") {
        alert("东西填完!");
        isCheckOk = false;
    }

    if (isCheckOk)
    if (isTime(ProgramTimeStart) && isTime(ProgramTimeEnd)) {
    } else {
        alert("时间格式有误!");
        isCheckOk = false;
    }

    if (isCheckOk && CheckTime(ProgramTimeStart, ProgramTimeEnd)) {
        alert("结束时间必需大于开始时间!");
        isCheckOk = false;
    }

    if (!isCheckOk) {
        $("#subCp").val("确 定");
        return false;
    }

    $.ajax({
        url: "/CourseProgram/EidtCourseProgram.aspx",
        data: {
            cpId: cpId,
            Id: Id,
            CourseType: trim(CourseType),
            ProgramTimeStart: ProgramTimeStart,
            ProgramTimeEnd: ProgramTimeEnd,
            ProgramName: trim(ProgramName),
            Property: trim(Property),
            ProgramUrl: trim(ProgramUrl),
            PassageType: PassageType,
            Weeks: week,
            SoftVersion: Version
        },
        type: "post",
        success: function (res) {
            if (res > 0) {
                if (cpId == "add") {    //新增
                    cpId = res;
                    var data = '[{"Id":' + res + ',"CourseType":"' + CourseType + '","ProgramTimeStart":"' + ProgramTimeStart + '","ProgramTimeEnd":"' + ProgramTimeEnd + '","ProgramName":"' + ProgramName + '","ProgramUrl":"' + ProgramUrl + '","Property":"' + Property + '","PassageType":"' + PassageType + '"}]';
                    var trHtml = FormatHtmlTr(eval(data), 0);
                    var thisTable = $("[week='" + week + "'] table");
                    var thisTableTrs = thisTable.find("[cpid][cpid!='add']"); //数据行的列表对象
                    if (thisTableTrs.length == 0) {   //没有数据行, 即休息, 新增将替换休息行
                        $("[week='" + week + "'] table").find("[rest]").replaceWith(trHtml);
                        trHtml = "";
                    }
                    thisTableTrs.each(function () {
                        if (CourseType == $(this).find("[mt='CourseType']").html()) {
                            var newTime = TimeToSum(ProgramTimeStart, ProgramTimeEnd);
                            var oldTime = TimeToSum($(this).find("[mt='ProgramTimeStart']").html(), $(this).find("[mt='ProgramTimeEnd']").html());
                            if (newTime < oldTime) {
                                $(this).before(trHtml);
                                trHtml = "";    //找到同类开始时间匹配的, 添加至前面, 清空trHtml
                            }
                        }
                    });
                    thisTable.find("[cpid='add']").before(trHtml);  //添加到新增按钮行前面
                    AddDblclick();
                } else {    //编辑
                    var thisTr = $("[cpid='" + cpId + "']");
                    thisTr.find("[mt='CourseType']").html(CourseType);
                    thisTr.find("[mt='ProgramTimeStart']").html(ProgramTimeStart);
                    thisTr.find("[mt='ProgramTimeEnd']").html(ProgramTimeEnd);
                    thisTr.find("[mt='ProgramName']").html(ProgramName);
                    thisTr.find("[mt='Property']").html(Property);
                    thisTr.find("[mt='ProgramUrl']").html(ProgramUrl);
                    thisTr.find("[mt='ProgramUrl']").attr("PassageType", PassageType);
                    if (PassageType == "1") //CMR通道颜色标识
                        thisTr.find("[mt='ProgramUrl']").attr("style", "color: #0074CC");
                    else
                        thisTr.find("[mt='ProgramUrl']").attr("style", "");

                    var thisTable = $("[week='" + week + "'] table");
                    var thisTableTrs = thisTable.find("[cpid][cpid!='add'][cpid!='" + cpId + "']"); //数据行的列表对象
                    var isBefore = false;

                    thisTableTrs.each(function () {
                        if (CourseType == $(this).find("[mt='CourseType']").html()) {
                            var newTime = TimeToSum(ProgramTimeStart, ProgramTimeEnd);
                            var oldTime = TimeToSum($(this).find("[mt='ProgramTimeStart']").html(), $(this).find("[mt='ProgramTimeEnd']").html());
                            if (newTime > oldTime) {
                                $(this).insertBefore(thisTr);   //移至编辑后时间的对应位置
                                isBefore = true;
                            }
                        }
                    });
                    if (!isBefore)
                        thisTable.find("[cpid='add']").insertBefore(thisTr);
                }
            } else
                alert("提交失败!")
            RemoveBackgroundColor();
            $("[cpId='" + cpId + "']").fadeOut(300).fadeIn(300).fadeOut(300).fadeIn(300);
            CloseNewConsulting();
        }
    });
}

//判断时间
function isTime(str) {
    var a = str.match(/^(\d{1,2})(:)?(\d{1,2})$/);
    if (a == null) 
        return false
    if (a[1] > 24 || a[3] > 60) 
        return false
    return true;
}

//开始时间大于等于结束时间
function CheckTime(timeStart, timeEnd) {
    var a = TimeToInt(timeStart);
    var b = TimeToInt(timeEnd);
    return a >= b;
}

function TimeToSum(timeStart, timeEnd) {
    var a = TimeToInt(timeStart);
    var b = TimeToInt(timeEnd);
    return a * 10000 + b;
}

function TimeToInt(myTime) {
    var strs = myTime.split(':');
    return parseInt(strs[0]) * 100 + parseInt(strs[1]);
}

//弹出编辑层
function OpenEdit(obj) {
    pos = GetObjPos(obj);
    var m = "mask";
    if (docEle("EditForm")) document.body.removeChild(docEle("EditForm"));
    if (docEle(m)) document.body.removeChild(docEle(m));

    var maxWidth = document.body.clientWidth - 250;
    var divLeft = pos.x;
    if (pos.x > maxWidth)
        divLeft = pos.x - (pos.x - maxWidth) - 20;

    var newDiv = document.createElement("div");
    newDiv.id = "EditForm";
    newDiv.style.position = "absolute";
    newDiv.style.zIndex = "990";
    newDiv.style.width = "400px";
    newDiv.style.height = "320px";
    newDiv.style.top = (pos.y - 140) + "px";
    newDiv.style.left = (divLeft + 40) + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "5px";
    newDiv.innerHTML = FormatEidtHTML(obj);
    document.body.appendChild(newDiv);
}

function FormatEidtHTML(obj) {
    var thisTr = $(obj).parents("tr");
    var cpId = thisTr.attr("cpId");
    var week = thisTr.parents("div").attr("week");
    var CourseType = "";
    var ProgramTimeStart = "";
    var ProgramTimeEnd = "";
    var ProgramName = "";
    var Property = "";
    var ProgramUrl = "";
    var PassageType = "";

    if (cpId != "add") {
        CourseType = thisTr.find("[mt='CourseType']").html();
        ProgramTimeStart = thisTr.find("[mt='ProgramTimeStart']").html();
        ProgramTimeEnd = thisTr.find("[mt='ProgramTimeEnd']").html();
        ProgramName = thisTr.find("[mt='ProgramName']").html();
        Property = thisTr.find("[mt='Property']").html();
        ProgramUrl = thisTr.find("[mt='ProgramUrl']").html();
        PassageType = thisTr.find("[mt='ProgramUrl']").attr("PassageType") == "1" ? "checked=\"checked\"" : "";
    }

    var res = ""; //<input type=\"text\" style=\"width:200px;\" id=\"ProgramTimeStart\" value=\"" + ProgramTimeStart + "\"/>/themes/default/scripts/jquery-easyui-1.3/themes/default/images/datebox_arrow.png
    res = "<div style=\"float:right;\"><span style=\"color:red;float:right;font-size: 20px;cursor: pointer;\" onclick=\"CloseNewConsulting()\"  title=\"关 闭\">X&nbsp;</span></div>" +
            "<div id=\"thisEdit\" style=\"padding:0 45px 0 0;width:100%;float:right;\">" +
                "<input id=\"eidtId\" type=\"hidden\" value=\"" + cpId + "\" />" +
                "<input id=\"eidtWeek\" type=\"hidden\" value=\"" + week + "\" />" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:11px 20px 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"CourseType\" value=\"" + CourseType + "\"/></div>" +
                    "<div style=\"float:right;padding:17px 20px 0 0;\">课程类型:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:184px;\" id=\"ProgramTimeStart\" value=\"" + ProgramTimeStart + "\"/><span onclick=\"EditTime(this)\" style=\"padding:5px 0 0 0;\"><img border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/default/images/datebox_arrow.png\"></span></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">开始时间:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:184px;\" id=\"ProgramTimeEnd\" value=\"" + ProgramTimeEnd + "\"/><span onclick=\"EditTime(this)\" style=\"padding:5px 0 0 0;\"><img border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/default/images/datebox_arrow.png\"></span></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">结束时间:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"ProgramName\" value=\"" + ProgramName + "\"/></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">节目名称:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"Property\" value=\"" + Property + "\"/></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">属 性:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:122px;\" id=\"ProgramUrl\" value=\"" + ProgramUrl + "\"/>&nbsp;&nbsp;CRM通道:<input type=\"checkbox\" id=\"PassageType\" " + PassageType + "/></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">房 号:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:29px 0 0 0;\">" +
                        "<input id=\"subCp\" type=\"button\" value=\"确 定\" style=\"width:80px;\" title=\"提 交\" />&nbsp;&nbsp;&nbsp;&nbsp;" +
                        //"<input id=\"cancel\" type=\"button\" value=\"取 消\" style=\"width:80px;\" onclick=\"CloseNewConsulting()\"/>" +
                    "</div>" +
                "</div>" +

            "</div>";
    return res;
}

//关闭编辑弹出层
function CloseNewConsulting() {
    RemoveBackgroundColor();
    document.body.removeChild(docEle("EditForm"));
}

function RemoveBackgroundColor() {
    $("[style='background-color: #ADDFF0']").attr("style", "");
}

function GetObjPos(ATarget) {
    var target = ATarget;
    var pos = new CPos(target.offsetLeft, target.offsetTop);

    var target = target.offsetParent;
    while (target) {
        pos.x += target.offsetLeft;
        pos.y += target.offsetTop + 10;

        target = target.offsetParent
    }

    return pos;
}

function docEle() {
    return document.getElementById(arguments[0]) || false;
}

function CPos(x, y) {
    this.x = x;
    this.y = y;
}

//加载所有版本
function LoadVersionList() {
    $.ajax({
        url: "/CourseProgram/GetVersion.aspx",
        data: { version: Version },
        type: "get",
        success: function (data) {
            $("#versionList").html(FormathtmlVersionList(eval(data)));
            $("#versionList a").first().click();    //加载第一个版本课程计划
        }
    });
}

function FormathtmlVersionList(data) {
    var res = ""
    for (var i = 0; i < data.length; i++) {
        if (res != "")
            res += " | "
        res += "<a v=\"" + data[i].SoftVersion + "\" href=\"javascript:void(0)\" onclick=\"GoVersion(this)\" class=\"l-btn l-btn-plain\" plain=\"true\">" + data[i].SoftVersion + "</a>"
    }
    return res;
    
}

//版本跳转
function GoVersion(o) {
    Version = $(o).html();  //设置全局版本值
    if ($("#cpOrder").is(":visible")) {
        GetProgramOrder();
    }
    LoadCourseProgram();    //加载课程计划
    $("#versionList").find("a").attr("style", "");
    $(o).attr("style", "border: 1px solid #7EABCD;background: url('/themes/default/scripts/jquery-easyui-1.3/themes/default/images/button_plain_hover.png') repeat-x left bottom;_padding: 0px 5px 0px 0px;-moz-border-radius: 3px;-webkit-border-radius: 3px;border-radius: 3px;");
}

//添加版本
function AddVersion() {
    var newVersion = prompt("请输入新增版本名称:", "");
    if (newVersion) {
        $("#versionList").append(" | <a v=\"" + newVersion + "\"  href=\"javascript:void(0)\" onclick=\"GoVersion(this)\" class=\"l-btn l-btn-plain\" plain=\"true\" >" + newVersion + "</a>");
        $("[v='" + newVersion + "']").click();
    }
}

//课程排序
function SetProgramOrder() {
    $("#cpOrder").is(":hidden") ? GetProgramOrder() : $("#cpOrder").slideUp(500);
}

//获取课程排序
function GetProgramOrder() { 
    $.ajax({
        url: "/CourseProgram/GetCourseType.aspx",
        data: { version: Version },
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
        res += "<li>" + data[i].CourseType + "</li>"
    }
    if (res == "")
        return "<span style=\"color: #C4C4BF;\">没东西</span>";
    return "<ul>" + res + "</ul><div><input id=\"submitCpOrder\" onclick=\"submitCpOrder()\" type=\"button\" value=\"确 定\" style=\"width:80px;\" title=\"提 交\" />&nbsp;&nbsp;&nbsp;&nbsp;<span>提示: 点击拖动</span></div>";
}

//提交排序变更
function submitCpOrder() {
    $("#submitCpOrder").val("提交中...");
    var cpo = "";
    $("#cpOrder li").each(function () {
        cpo += ((cpo == "" ? "" : ",") + $(this).html() + "|" + $(this).attr("data-itemidx"));
    });
    $.ajax({
        url: "/CourseProgram/UpdateCourseType.aspx",
        data: { version: Version, cpo: cpo },
        type: "post",
        success: function (res) {
            $("#submitCpOrder").val("提交成功");
            if (res == 0)
                $("#submitCpOrder").val("没变更");
            $("#cpOrder").slideUp(500);
        }
    });
}

function EditTime(obj) {
    //alert($(obj).parent().find("input").val());

    OpenTimeEdit(obj);

    var times = $(obj).parent().find("input").val().split(':');
    $("[#times [hh='" + times[0] + "']").addClass("selthisTime");
    $("[#times [mm='" + times[1] + "']").addClass("selthisTime");
    $("#subTime").click(function () {
        subTime(obj);
    });

    $("#hoursstart li").each(function () {
        $(this).click(function () {
            $(this).addClass("selthisTime").siblings().removeClass("selthisTime");
            var i = parseInt($("#subTime").attr("csum"));
            i--;
            $("#subTime").attr("csum",i);
            if (i == 0)
                subTime(obj);
        });
    });
    $("#minutesend li").each(function () {
        $(this).click(function () {
            $(this).addClass("selthisTime").siblings().removeClass("selthisTime");
            var i = parseInt($("#subTime").attr("csum"));
            i--;
            $("#subTime").attr("csum", i);
            if (i == 0)
                subTime(obj);
        });
    });
}

//弹出时间选择层
function OpenTimeEdit(obj) {
    pos = GetObjPos(obj);
    var m = "mask";
    if (docEle("TimeEditForm")) document.body.removeChild(docEle("TimeEditForm"));
    if (docEle(m)) document.body.removeChild(docEle(m));

    var maxWidth = document.body.clientWidth - 250;
    var divLeft = pos.x;
    if (pos.x > maxWidth)
        divLeft = pos.x - (pos.x - maxWidth) - 20;

    var newDiv = document.createElement("div");
    newDiv.id = "TimeEditForm";
    newDiv.style.position = "absolute";
    newDiv.style.zIndex = "991";
    newDiv.style.width = "270px";
    newDiv.style.height = "300px";
    newDiv.style.top = (pos.y + 7) + "px";
    newDiv.style.left = (divLeft -150) + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "5px";
    newDiv.innerHTML = FormatTimeEditHtml(obj);
    document.body.appendChild(newDiv);
}

function FormatTimeEditHtml(obj) {
    var res = "<div style=\"float:right;\"><span style=\"color:red;float:right;font-size: 20px;cursor: pointer;margin-top: -4px;\" onclick=\"CloseTimeConsulting()\"  title=\"关 闭\">X</span></div>" +
                "<div class=\"selTime\" id=\"times\" style=\"display:block; width:36px;padding-left:20px;\">" +
                    "<div>小时</div>" +
                    "<ul id=\"hoursstart\">" +
                    getTimeHtml(24, "hh") +
                    "</ul>" +
                 "<div>分钟</div>" +
                "<ul id=\"minutesend\">" +
                    getTimeHtml(60, "mm") +
                    "</ul>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:10px 5px 0 0;\">" +
                        "<input id=\"subTime\" csum=\"2\" type=\"button\" value=\"确 定\" style=\"width:80px;\" />&nbsp;&nbsp;" +
                        //"<input id=\"cancelTime\" type=\"button\" value=\"取 消\" style=\"width:80px;\" onclick=\"CloseTimeConsulting()\"/>" +
                    "</div>" +
                "</div>";
    return res;
}

function subTime(obj) {
    $(obj).parent().find("input").val($("#hoursstart [class='selthisTime']").html() + ":" + $("#minutesend [class='selthisTime']").html());
    CloseTimeConsulting();
}

function getTimeHtml(con, t) {
    var minutes = "";
    for (var i = 0; i < con; i++) {
        if (i < 10)
            minutes += "<li " + t + "= \"0" + i + "\">0" + i + "</li>";
        else
            minutes += "<li " + t + " = \"" + i + "\">" + i + "</li>";
    }
    return minutes;
}

//关闭时间选择层
function CloseTimeConsulting() {
    document.body.removeChild(docEle("TimeEditForm"));
}

//双击行触发编辑点击事件
function AddDblclick() {
    $("#cpList").find("[cpid]").unbind("dblclick");
    $("#cpList").find("[cpid]").dblclick(function () {
        $(this).find("[edit]").click();
    });

    $("[cpid]").mouseover(function () {
        $("[cpid]").removeClass("selbg");
        $(this).addClass("selbg");
    });

    $("#cpList table").each(function () {
        $(this).find("[cpid][cpid!='add']").removeClass("mybg");
        $(this).find("[cpid][cpid!='add']:odd").addClass("mybg");
    });

}
////////////////////////////////////////////////////
//加载推荐
function LoadCourseRecommend() {
    $.ajax({
        url: "/CourseProgram/GetCourseRecommend.aspx",
        data: { version: Version },
        type: "get",
        success: function (data) {
            $("#cpList").prepend(FormatCrHtml(eval(data)));
            $("#cpList").find("[crId]").dblclick(function () {
                $(this).find("[editCr]").click();
            });
        }
    });
}

function FormatCrHtml(data) {
    var crId = "";
    var Title = "";
    var DateStart = "";
    var DateTimeStart = "";
    var DateTimeEnd = "";
    var Introduction = "";
    var RoomId = "";
    var Status = "0";
    var PassageType = "";

    if(data.length > 0){
        crId = data[0].Id;
        Title = data[0].Title;
        DateStart = data[0].DateStart;
        DateTimeStart = data[0].DateTimeStart;
        DateTimeEnd = data[0].DateTimeEnd;
        Introduction = data[0].Introduction;
        RoomId = data[0].RoomId;
        Status = data[0].Status;
        PassageType = data[0].PassageType;
    }

    var Recommend = "";
    var unRecommend = "";
    Status == "1" ? unRecommend = "style=\"display:none;\"" : Recommend = "style=\"display:none;\"";
        

    res = "<div style=\"padding: 50px 20px 0 30px; font-size: 12px;\">" +
                "<h2 style=\"color:#E59E52; padding: 5px 0;\">推 荐</h2>" +
                "<table cellpadding=\"0\" cellspacing=\"0\" class=\"c_table\">" +
                "<tr style=\"background-color:#F7E1CA;\">" +
                    "<th width=\"5%\" nowrap>&nbsp;</th>" +
                    "<th width=\"15%\" nowrap>标 题</th>" +
                    "<th width=\"10%\" nowrap>日 期</th>" +
                    "<th width=\"5%\" nowrap>开 始</th>" +
                    "<th width=\"5%\" nowrap>结 束</th>" +
                    "<th width=\"45%\" nowrap>简 介</th>" +
                    "<th width=\"10%\" nowrap>房 号</th>" +
                    "<th width=\"5%\" style=\"border-right: 1px solid #ccc;\" nowrap>&nbsp;</th>" +
                "</tr>" +
                "<tr crId=\"" + crId + "\" class=\"td_ys\">" +
                "<td width=\"5%\" align=\"center\"><a editCr=\"editCr\" title=\"编辑\" href=\"javascript:void(0)\" onclick=\"EditCr(this)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/icons/pencil.png\"></a></td>" +
                "<td mt=\"Title\" width=\"15%\" align=\"center\">" + Title + "</td>" +
                "<td mt=\"DateStart\" width=\"10%\" align=\"center\">" + DateStart + "</td>" +
                "<td mt=\"DateTimeStart\" width=\"5%\" align=\"center\">" + DateTimeStart + "</td>" +
                "<td mt=\"DateTimeEnd\" width=\"5%\" align=\"center\">" + DateTimeEnd + "</td>" +
                "<td mt=\"Introduction\" width=\"45%\" align=\"left\" title=\"双击编辑\">" + Introduction + "</td>" +
                "<td mt=\"RoomId\" width=\"10%\" align=\"center\" PassageType=\"" + PassageType + "\">" + RoomId + "</td>" +
                "<td width=\"5%\" style=\"border-right: 1px solid #ccc;\" align=\"center\"><a " + Recommend + " title=\"点击取消推荐\" crStatus=\"1\" href=\"javascript:void(0)\" onclick=\"DelCr('" + crId + "',0)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/images/Recommend.gif\"></a><a " + unRecommend + " title=\"点击推荐\" crStatus=\"0\" href=\"javascript:void(0)\" onclick=\"DelCr('" + crId + "',1)\"><img width=\"16\" height=\"16\" border=\"0\" src=\"/themes/default/images/unRecommend.gif\"></a></td>" +
            "</tr>" +
            "</table></div>";
    return res;
}

function DelCr(crid, status) {
    if (crid == "")
        alert("没有推荐信息");
    else {
        $.ajax({
            url: "/CourseProgram/UpDataCourseRecommendStatus.aspx",
            data: { crid: crid, status: status, version: Version },
            type: "get",
            success: function (res) {
                if (res > 0) {
                    $("[crStatus]").hide();
                    $("[crStatus='" + status + "']").show();
                }
            }
        });
    }
}

//编辑推荐
function EditCr(obj) {
    OpenEditCr(obj);
    $.parser.parse();
    $("#subCr").click(function () {
//        if ($(this).val() == "提交中...")
//            return false;
//        $(this).val("提交中...")
        EditCrSubmit();
    });
}

//提交变更
function EditCrSubmit() {
    var Title = $("#Title").val();
    //var DateStart = $("#DateStart").val();
    var DateStart = $("[class='combo-value']").val();
    var DateTimeStart = $("#DateTimeStart").val();
    var DateTimeEnd = $("#DateTimeEnd").val();
    var Introduction = $("#Introduction").val();
    var RoomId = $("#RoomId").val();
    var Status = $("#Status").attr("checked") == "checked" ? "1" : "0";
    var PassageType = $("#PassageType").attr("checked") == "checked" ? "1" : "0";
    var isCheckOk = true;

    if (Title == "" || DateStart == "" || DateTimeStart == "" || DateTimeEnd == "" || Introduction  == "") {
        alert("东西填完!");
        isCheckOk = false;
    }

    if (isCheckOk)
    if (isTime(DateTimeStart) && isTime(DateTimeEnd)) {
    } else {
        alert("时间格式有误!");
        isCheckOk = false;
    }

    if (isCheckOk && CheckTime(DateTimeStart, DateTimeEnd)) {
        alert("结束时间必需大于开始时间!");
        isCheckOk = false;
    }

    if (!isCheckOk) {
        $("#subCr").val("确 定");
        return false;
    }

    $.ajax({
        url: "/CourseProgram/UpDataCourseRecommend.aspx",
        data: {
            Title: trim(Title),
            DateStart: DateStart,
            DateTimeStart: DateTimeStart,
            DateTimeEnd: DateTimeEnd,
            Introduction: Introduction,
            RoomId: trim(RoomId),
            Status: Status,
            PassageType: PassageType,
            SoftVersion: Version
        },
        type: "post",
        success: function (res) {
            if (res > 0) {
                var thisTr = $("[crId]");
                thisTr.find("[mt='Title']").html(Title);
                thisTr.find("[mt='DateStart']").html(DateStart);
                thisTr.find("[mt='DateTimeStart']").html(DateTimeStart);
                thisTr.find("[mt='DateTimeEnd']").html(DateTimeEnd);
                thisTr.find("[mt='Introduction']").html(Introduction);
                thisTr.find("[mt='RoomId']").html(RoomId);
                thisTr.find("[mt='RoomId']").attr("PassageType", PassageType);
                $("[crStatus='1']").attr("onclick", "DelCr('" + res + "',0)");
                $("[crStatus='0']").attr("onclick", "DelCr('" + res + "',1)");
                $("[crStatus]").hide();
                $("[crStatus='" + Status + "']").show();
            } else
                alert("提交失败!")
            CloseNewConsultingCr();
            thisTr.fadeOut(300).fadeIn(300).fadeOut(300).fadeIn(300);
        }
    });
}

//弹出编辑层
function OpenEditCr(obj) {
    pos = GetObjPos(obj);
    var m = "mask";
    if (docEle("EditFormCr")) document.body.removeChild(docEle("EditForm"));
    if (docEle(m)) document.body.removeChild(docEle(m));

    var maxWidth = document.body.clientWidth - 250;
    var divLeft = pos.x;
    if (pos.x > maxWidth)
        divLeft = pos.x - (pos.x - maxWidth) - 20;

    var newDiv = document.createElement("div");
    newDiv.id = "EditFormCr";
    newDiv.style.position = "absolute";
    newDiv.style.zIndex = "990";
    newDiv.style.width = "450px";
    newDiv.style.height = "400px";
    newDiv.style.top = (pos.y - 140) + "px";
    newDiv.style.left = (divLeft + 40) + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "5px";
    newDiv.innerHTML = FormatEidtHTMLCr(obj);
    document.body.appendChild(newDiv);
}

function FormatEidtHTMLCr(obj) {
    var thisTr = $(obj).parents("tr");
    var cpId = thisTr.attr("cpId");
    var week = thisTr.parents("div").attr("week");
    var Title = "";
    var DateStart = "";
    var DateTimeStart = "";
    var DateTimeEnd = "";
    var Introduction = "";
    var RoomId = "";
    var Status = "";
    var PassageType = "";

    
    Title = thisTr.find("[mt='Title']").html();
    DateStart = thisTr.find("[mt='DateStart']").html();
    DateTimeStart = thisTr.find("[mt='DateTimeStart']").html();
    DateTimeEnd = thisTr.find("[mt='DateTimeEnd']").html();
    Introduction = thisTr.find("[mt='Introduction']").html();
    RoomId = thisTr.find("[mt='RoomId']").html();
    Status = $("[crStatus]:visible").attr("crStatus") == "1" ? "checked=\"checked\"" : "";
    PassageType = thisTr.find("[mt='RoomId']").attr("PassageType") == "1" ? "checked=\"checked\"" : "";


    var res = ""; 
    res = "<div style=\"float:right;\"><span style=\"color:red;float:right;font-size: 20px;cursor: pointer;\" onclick=\"CloseNewConsultingCr()\"  title=\"关 闭\">X&nbsp;</span></div>" +
            "<div id=\"thisEdit\" style=\"padding:0 45px 0 0;width:100%;float:right;\">" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:11px 20px 0 0;\"><input type=\"text\" maxlength=\"20\" style=\"width:250px;\" id=\"Title\" value=\"" + Title + "\"/></div>" +
                    "<div style=\"float:right;padding:17px 20px 0 0;\">标 题:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:254px;\" id=\"DateStart\" class=\"easyui-datebox\" value=\"" + DateStart + "\"/></div>" + //timespinner
                    "<div style=\"float:right;padding:19px 20px 0 0;\">日 期:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:234px;\" id=\"DateTimeStart\" value=\"" + DateTimeStart + "\"/><span onclick=\"EditTime(this)\" style=\"padding:5px 0 0 0;\"><img border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/default/images/datebox_arrow.png\"></span></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">开始时间:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:234px;\" id=\"DateTimeEnd\" value=\"" + DateTimeEnd + "\"/><span onclick=\"EditTime(this)\" style=\"padding:5px 0 0 0;\"><img border=\"0\" src=\"/themes/default/scripts/jquery-easyui-1.3/themes/default/images/datebox_arrow.png\"></span></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">结束时间:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><textarea style=\"width:248px;\" id=\"Introduction\" rows=\"5\" cols=\"10\">" + Introduction + "</textarea></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">简 介:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 20px 0 0;\"><input type=\"text\" style=\"width:172px;\" id=\"RoomId\" value=\"" + RoomId + "\"/>&nbsp;&nbsp;CRM通道:<input type=\"checkbox\" id=\"PassageType\" " + PassageType + "/></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">房 号:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:13px 255px 0 0;\"><input type=\"checkbox\" id=\"Status\" " + Status + "/></div>" +
                    "<div style=\"float:right;padding:19px 20px 0 0;\">启用推荐:</div>" +
                "</div>" +
                "<div style=\"width:100%;float:left;\">" +
                    "<div style=\"float:right;padding:29px 0 0 0;\">" +
                        "<input id=\"subCr\" type=\"button\" value=\"确 定\" style=\"width:80px;\" title=\"提 交\" />&nbsp;&nbsp;&nbsp;&nbsp;" +
                    "</div>" +
                "</div>" +

            "</div>";
    return res;
}

//关闭编辑弹出层
function CloseNewConsultingCr() {
    document.body.removeChild(docEle("EditFormCr"));
}

//去除左右空格
function trim(s) {
    if (s == null)
        return "";
    return trimRight(trimLeft(s));
}  