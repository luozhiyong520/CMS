function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

$(function () {

    if (getQueryString("PopupType") == "edit") {
        $("#submit").val("提交并新增消息");
        lookpopup(getQueryString("PlanId"));
    }
    SetGroup();

    $("#upfile").change(function () {
        $("#submit").attr("disabled", true);
        uploadfile();
    });

    $("#submit").click(function () {

        $("[name='ReceiverTypeSel']").each(function () {
            if ($(this).attr("checked") == "checked")
                $("#ReceiverType").val($(this).val());
        });

        $("#submit").attr("disabled", true);
        var Title = $("#Title").val();
        var Content = $("#Content").val();
        var ImgUrl = $("#upfile").val();
        var ReceiverType = $("#ReceiverType").val();
        var Receiver = $("#Receiver").val();
        var BeginTime = $("#BeginTime").val();
        var EndTime = $("#EndTime").val();

        var myDate = new Date();
        var nowTime = myDate.getFullYear() + "-" + ((myDate.getMonth() + 1) < 10 ? ("0" + (myDate.getMonth() + 1)) : (myDate.getMonth() + 1)) + "-" + (myDate.getDate() < 10 ? ("0" + myDate.getDate()) : (myDate.getDate())) + " " + (myDate.getHours() < 10 ? ("0" + myDate.getHours()) : myDate.getHours()) + ":" + (myDate.getMinutes() < 10 ? ("0" + myDate.getMinutes()) : myDate.getHours()) + ":" + (myDate.getSeconds() < 10 ? ("0" + myDate.getSeconds()) : myDate.getSeconds());

        if (BeginTime != "" && EndTime != "") {
            if (comptime(BeginTime, EndTime)) {
                alert("结束时间必需大于开始时间!");
                $("#submit").attr("disabled", false);
                return false;
            }
        }

        if (comptime(nowTime, EndTime) && EndTime != "") {
            alert("结束时间必需大于当前时间!");
            $("#submit").attr("disabled", false);
            return false;
        }

        if (Content == "" && ImgUrl == "" && $("#inputImgUrl").attr("src") == "/themes/default/images/u0_normal.png") {
            alert("请填写必要信息, 正文或上传图片!");
            $("#submit").attr("disabled", false);
            return false;
        } else if (ReceiverType == 0) {
            if ($("#ulReceiverGroup input:checked").length < 1) {
                alert("请选择用户组!");
                $("#submit").attr("disabled", false);
                return false;
            }
        } else if (ReceiverType == 1) {
            if (Receiver == "" || Receiver == "请输入用户名，多个用户名请使用,分隔") {
                alert("请录入接收对象!");
                $("#submit").attr("disabled", false);
                return false;
            }
        } else if (ReceiverType == 2) {
            if ($("#ulReceiverGroup1").val() == "") {
                alert("请选择用户组!");
                $("#submit").attr("disabled", false);
                return false;
            }
        }

        AddPopup();

    });

    $("#PlanType1").click(function () {
        $("#BeginTime").val("");
    });

    $("#GroupAll").click(function () {
        if ($(this).attr("checked") == "checked") {
            $("#ulReceiverGroup :checkbox").attr("checked", true);
        }
        else {
            $("#ulReceiverGroup :checkbox").attr("checked", false);
        }
    });

    $("#a").focus(function () {
        if ($(this).val() == "接收对象描述")
            $(this).val("");
    });

    $("#a").blur(function () {
        if ($(this).val() == "")
            $(this).val("接收对象描述");
    });

    $("#Receiver").focus(function () {
        if ($(this).val() == "请输入用户名，多个用户名请使用,分隔")
            $(this).val("");
    });

    $("#Receiver").blur(function () {
        if ($(this).val() == "")
            $(this).val("请输入用户名，多个用户名请使用,分隔");
    });

    $("[name='ReceiverTypeSel']").click(function () {
        if ($(this).val() == 0) {
            $("#ReceiverType0").show();
            $("#ReceiverType1").hide();
            $("#ReceiverType").val(0);
            $("#ReceiverType2").hide();
            $("#submit").attr("disabled", false);
            $("#tongbu").hide();
        } else if ($(this).val() == 1) {
            $("#ReceiverType0").hide();
            $("#ReceiverType1").show();
            $("#ReceiverType").val(1);
            $("#ReceiverType2").hide();
            $("#submit").attr("disabled", false);
            $("#tongbu").hide();
        } else if ($(this).val() == 2) {
            GetGroup();
            $("#ReceiverType0").hide();
            $("#ReceiverType1").hide();
            $("#ReceiverType").val(2);
            $("#ReceiverType2").show();
            $("#submit").attr("disabled", true);
            $("#tongbu").show();

        }
    });



    $("[name='PlanTypeSel']").click(function () {
        if ($(this).val() == 0) {
            $("#PlanType").val(0);
            $("#BeginTime").hide();
        } else if ($(this).val() == 1) {
            $("#PlanType").val(1);
            $("#BeginTime").show();
        }
    });

    $("#tongbu").click(function () {
        var groupId = $("#ulReceiverGroup1").val();
        var groupName = $("#ulReceiverGroup1 option:selected").text();
        $("#tongbu").attr("disabled", true);
        $("#submit").attr("disabled", true);
        $("#tongbu").val("正在同步，请稍后...");
        $.ajax({
            url: "/AjaxCustomerGroup/GetUserDataList.cspx",
            data: { "groupId": groupId, "groupName": groupName },
            type: "POST",
            success: function (res) {
                if (res == "000001") {
                    alert("非法请求");
                } else if (res == "000002") {
                    alert("没有获取到数据");
                    $("#submit").attr("disabled", true);
                }
                else {
                    alert("同步成功,请继续下面操作!");
                    $("#tongbu").attr("disabled", false);
                    $("#submit").attr("disabled", false);
                    $("#tongbu").val("同步");
                }
            }
        });


    })

    $("#BeginTime,#EndTime").datetimepicker({
        showSecond: true,
        timeFormat: 'hh:mm:ss',
        stepHour: 1,
        stepMinute: 1,
        stepSecond: 1
    });

})


function GetGroup() {
    $.ajax({
        url: "/AjaxCustomerGroup/GetGroupList.cspx",
        type: "GET",
        success: function (res) {
            if (res == "000001") {
                alert("非法请求");
            } else if (res == "000002") {
                alert("没有获取到数据");
                $("#submit").attr("disabled", true);
            } 
            else {
                var groups = res.split("\r\n");
                var groupHtml = "";
                for (var i = 0; i < groups.length - 1; i++) {
                    var grouparr = groups[i].split(",");
                    groupHtml += "<option  value='" + grouparr[0] + "'>" + grouparr[1] + "</option>";
                }
                $("#ulReceiverGroup1").html(groupHtml);
            }
        }
    });
}


function SetGroup() {
    $.ajax({
        url: "/AjaxPopup/GetGroup.cspx",
        data: {},
        type: "POST",
        success: function (res) {
            var groups = res.split(",");
            var groupHtml = "";
            for (var i = 0; i < groups.length; i++) {
                groupHtml += "<li><label><input type=\"checkbox\" name=\"ReceiverGroup\" value=\"" + groups[i] + "\" /> " + groups[i] + "</label></li>";
            }
            $("#ulReceiverGroup").html(groupHtml);
        }
    });
}

function AddPopup() {

    var Title = $("#Title").val();
    var Content = $("#Content").val();
    var ImgUrl = $("#inputImgUrl").attr("src");
    var PageUrl = $("#PageUrl").val();
    var PlanType = $("#PlanType").val();
    var ReceiverType = $("#ReceiverType").val();
    var Receiver = $("#Receiver").val();
    var BeginTime = $("#BeginTime").val();
    var EndTime = $("#EndTime").val();

    if (ImgUrl == "/themes/default/images/u0_normal.png")
        ImgUrl = "";
    
    if (ReceiverType == 0) {
        var str = "";
        $("#ulReceiverGroup input:checked").each(function () {
            if (str == "")
                str = $(this).val()
            else
                str += "," + $(this).val();
        });
        Receiver = str;
    }else if (ReceiverType == 2) {
        Receiver = $("#ulReceiverGroup1 option:selected").text();
     }

 
    $.ajax({
        url: "/AjaxPopup/AddPopup.cspx",
        data: { "Title": Title,
            "Content": Content,
            "ImgUrl": ImgUrl,
            "PageUrl": PageUrl,
            "PlanType": PlanType,
            "ReceiverType": ReceiverType,
            "Receiver": Receiver,
            "BeginTime": BeginTime,
            "EndTime": EndTime,
            "type": "add"
        },
        type: "POST",
        success: function (res) {
            if (res > 0)
                alert("提交成功!");
            self.parent.document.getElementById("PopupMsgInfo").style.display = 'none';
            self.parent.window.location = self.parent.window.location;
             
        }
    });
}

//上传文件
function uploadfile() {
    //上传控件
    var imgupfile = $("#upfile");
    var span = imgupfile.parent();

    if (imgupfile.val() == "")
        return false;
    else
        if (!checkImgType(imgupfile.val())) {
            alert("格式不正确,只能上传格式为gif|jpeg|jpg|png|bmp！");
            return false;
        }

    //准备表单, 将表单加到document上
    var myform = document.createElement("form");
    myform.action = "/AjaxVisualEditing/Upload.cspx";
    myform.method = "post";
    myform.enctype = "multipart/form-data";
    myform.style.display = "none";
    document.body.appendChild(myform);
    var form = $(myform);

    //上传控件附加到form中
    imgupfile.appendTo(form);

    span.html("<img src=\"/themes/default/images/loadings.gif\" />&nbsp;&nbsp;&nbsp;正在上传...&nbsp;&nbsp;");

    form.ajaxSubmit({
        success: function (data) {
            if (data == "NoFile" || data == "Error" || data == "格式不正确！") {
                alert(data);
                return false;
            }
            //$("#ImgUrl").val(data);
            form.remove();
            span.html("<input type=\"file\" name=\"file\" id=\"upfile\" class=\"w300 inTextbox\" />");
            $("#upfile").change(function () {
                $("#submit").attr("disabled", true);
                uploadfile();
            });
            data = data.replace("<PRE>", "");
            data = data.replace("</PRE>", "");
            $("#inputImgUrl").attr("src", data);
            $("#submit").attr("disabled", false);
        }
    });
}

//检查上传的图片格式
function checkImgType(filename) {
    var pos = filename.lastIndexOf(".");
    var str = filename.substring(pos, filename.length)
    var str1 = str.toLowerCase();
    if (!/\.(gif|jpg|jpeg|png|bmp)$/.test(str1)) {
        return false;
    }
    return true;
}


function lookpopup(PlanId) {
    $("#LookPopu :text").val("");
    $("#LookPopu :input").attr("checked", false);
    $("#Receiver").val("请输入用户名，多个用户名请使用,分隔");

    $.ajax({
        url: "/AjaxPopupMsgPlan/GetPopupMsgPlan.cspx?PlanId=" + PlanId,
        success: function (json) {
            $("#Title").val(json.Title);
            $("#Content").val(json.Content);
            $("#inputImgUrl").attr("src", json.ImgUrl);
            $("#PageUrl").val(json.PageUrl);

            if (json.ReceiverType == 0) {
                $("#RadioGroup1_0").attr("checked", "checked");
                $("#ReceiverType0").css("display", "block");
                $("#ReceiverType1").css("display", "none");
                $("#ReceiverType2").css("display", "none");
                $.ajax({
                    url: "/AjaxPopup/GetGroup.cspx",
                    data: {},
                    type: "POST",
                    success: function (res) {
                        var groups = res.split(",");
                        var groupHtml = "";
                        for (var i = 0; i < groups.length; i++) {
                            var checked = "";
                            var rgroups = json.Receiver.split(",");
                            for (var j = 0; j < rgroups.length; j++) {
                                if (rgroups[j] == groups[i]) {
                                    checked = "checked"
                                } 
                            }
                            groupHtml += "<li><label><input type=\"checkbox\" name=\"ReceiverGroup\" value=\"" + groups[i] + "\" " + checked + "/> " + groups[i] + "</label></li>";
                        }
                        $("#ulReceiverGroup").html(groupHtml);
                    }
                });


            } else if (json.ReceiverType == 1) {
                $("#RadioGroup1_1").attr("checked", "checked");
                $("#ReceiverType1").css("display", "block");
                $("#ReceiverType0").css("display", "none");
                $("#ReceiverType2").css("display", "none");
                $("#Receiver").val(json.Receiver);
            } else if (json.ReceiverType == 2) {
                $("#RadioGroup1_2").attr("checked", "checked");
                $("#ReceiverType0").css("display", "none");
                $("#ReceiverType1").css("display", "none");
                $("#ReceiverType2").css("display", "block");
                $.ajax({
                    url: "/AjaxCustomerGroup/GetGroupList.cspx",
                    type: "GET",
                    success: function (res) {
                        var groups = res.split("\r\n");
                        var groupHtml = "";
                        for (var i = 0; i < groups.length - 1; i++) {
                            var grouparr = groups[i].split(",");
                            if (json.Receiver == grouparr[1]) {
                                groupHtml += "<option  value='" + grouparr[0] + "' selected>" + grouparr[1] + "</option>";
                            } else {
                                groupHtml += "<option  value='" + grouparr[0] + "'>" + grouparr[1] + "</option>";
                            }
                        }
                        $("#ulReceiverGroup1").html(groupHtml);
                    }
                });
                $("#tongbu").css("display", "");
            }

          //  if (json.PlanType == 0) {
          //      $("#PlanType1").attr("checked", "checked");
         //   } else {
                $("#PlanType2").attr("checked", "checked");
                $("#BeginTime").css("display", "block");
                if (json.BeginTime != null)
                    $("#BeginTime").val(ConertJsonTimeAndFormat(json.BeginTime, 'yyyy-MM-dd hh:mm:ss'));
                if (json.EndTime != null)
                    $("#EndTime").val(ConertJsonTimeAndFormat(json.EndTime, 'yyyy-MM-dd hh:mm:ss'));
          //  }

        }
    });
    }

    //js时间比较(yyyy-mm-dd hh:mi:ss)
    function comptime(beginTime, endTime) {
        //alert(beginTime + " | " + endTime);
//        var beginTime = "2009-09-21 00:00:00";
//        var endTime = "2009-09-21 00:00:01";
        var beginTimes = beginTime.substring(0, 10).split('-');
        var endTimes = endTime.substring(0, 10).split('-');

        beginTime = beginTimes[1] + '-' + beginTimes[2] + '-' + beginTimes[0] + ' ' + beginTime.substring(10, 19);
        endTime = endTimes[1] + '-' + endTimes[2] + '-' + endTimes[0] + ' ' + endTime.substring(10, 19);

//        alert(beginTime + "aaa" + endTime);
//        alert(Date.parse(endTime));
//        alert(Date.parse(beginTime));
        var a = (Date.parse(endTime) - Date.parse(beginTime)) / 3600 / 1000;
        if (a < 0) {
            //alert("endTime小!");
            return true;
        } else if (a > 0) {
            //alert("endTime大!");
            return false;
        } else if (a == 0) {
            //alert("时间相等!");
            return true;
        } else {
            return 'exception'
        }
    }
