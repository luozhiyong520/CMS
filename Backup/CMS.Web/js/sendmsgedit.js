$(function () {
    $("#btnReply").css("display", "none");
    if ($("#txtReplyContent").html() == null || $("#txtReplyContent").html() == "") {
        $("#txtReplyContent").removeAttr("disabled");
        $("#btnReply").css("display", "block");
    }
    $("#btnReply").click(function () {
        var msgid = $("#hdMsgId").val();
        var replyContent = $("#txtReplyContent").val();
        var replier = $("#txtReplier").val();
        var replyTime = $("#txtReplyTime").val();
        if (ValidateForm()) {
            $.ajax({
                url: '/AjaxSendMsg/UpdateSendMsgById.cspx',
                type: 'POST',
                data: { 'msgid': msgid, 'replyContent': replyContent, 'replier': replier, 'replyTime': replyTime },
                success: function (responsevalue) {
                    if (responsevalue > 0) {
                        alert('回复留言成功！');
                        window.returnValue = responsevalue;
                        window.close();
                        // window.opener.location.reload();

                    }
                }
            });
        }
    });
    //上一题 
    $("#btnback").click(function () {
        $(this).css("display", "block");
        $("#btngo").css("display", "block");
        $("#btnReply").css("display", "none");
        var msgid = $("#hdMsgId").val();
        var hdParamList = $("#hdParamsList").val();
        if ($("#txtReplyContent").html() == null || $("#txtReplyContent").html() == "") {
            $("#txtReplyContent").removeAttr("disabled");
            $("#btnReply").css("display", "block");
        } else {
            $("#btnReply").css("display", "none");
        }
        if (hdParamList != "" || hdParamList != null) {
            var paramlist = hdParamList.split(',');
            for (var i = 0; i < paramlist.length; i++) {
                if (paramlist[i] == msgid) {
                    $.ajax({
                        url: '/AjaxSendMsg/getSendMsgbackById.cspx',
                        type: 'POST',
                        data: { 'msgid': msgid, 'MsgIds': hdParamList },
                        success: function (responsevalue) {
                            var svalue = eval(responsevalue);
                            if (svalue == "" || svalue == null) {

                                alert('已经当前页是第一题了！');
                            } else {

                                if (svalue.MsgId != null) {
                                    formClear();
                                    var ReplyTime = "";
                                    if (svalue.ReplyTime == null || svalue.ReplyTime == "") {

                                    } else {
                                        ReplyTime = ConertJsonTimeAndFormat(svalue.ReplyTime, 'yyyy-MM-dd hh:mm:ss');
                                    }
                                    var QuTime = ConertJsonTimeAndFormat(svalue.QuTime, 'yyyy-MM-dd hh:mm:ss');
                                    $("#lbHardwareVersion").html(svalue.HardwareVersion);
                                    $("#lbSoftwareVersion").html(svalue.SoftwareVersion);
                                    $("#hdMsgId").val(svalue.MsgId);
                                    $("#lbQuTime").html(QuTime);
                                    if (svalue.ImgUrl != '' || svalue.ImgUrl != null) {
                                        $("#ImgUrl").attr('src', 'http://img.upchina.com' + svalue.ImgUrl);
                                    }
                                    $("#lbTypeContent").html(svalue.TypeContent);
                                    $("#lbQuCustomerName").html(svalue.QuCustomerName);
                                    $("#lbMsgContent").html(svalue.MsgContent);
                                    $("#txtReplyContent").html(svalue.ReplyContent);
                                    if (svalue.ReplyContent == null || svalue.ReplyContent == "") {
                                        $("#txtReplyContent").removeAttr("disabled");
                                        $("#btnReply").css("display", "block");
                                    } else {
                                        $("#txtReplyContent").attr("disabled", "disabled");
                                        $("#btnReply").css("display", "none");
                                    }
                                    $("#txtReplier").val(svalue.Replier);
                                    $("#txtReplyTime").val(ReplyTime);
                                    var tel = (svalue.Tel == '' || svalue.Tel == null || svalue.Tel == undefined) ? '' : (svalue.Tel.substring(0, svalue.Tel.length - 4) + '****');
                                    $("#lbTel").html('');
                                    $("#lbTel").html(tel);
                                    $("#ImgUrl").attr("src", (svalue.ImgUrl == null || svalue.ImgUrl == "" || svalue.ImgUrl == undefined) ? "" : "http://img.upchina.com" + svalue.ImgUrl)
                                }
                            }
                        }
                    });
                }
            }
        }

    });
    //下一题
    $("#btngo").click(function () {
        $(this).css("display", "block");
        $("#btnback").css("display", "block");
        $("#btnReply").css("display", "none");
        var msgid = $("#hdMsgId").val();
        var hdParamList = $("#hdParamsList").val();
        if ($("#txtReplyContent").html() == null || $("#txtReplyContent").html() == "") {
            $("#txtReplyContent").removeAttr("disabled");
            $("#btnReply").css("display", "block");
        } else {
            $("#btnReply").css("display", "none");
        }
        if (hdParamList != "" || hdParamList != null) {
            var paramlist = hdParamList.split(',');
            for (var i = 0; i < paramlist.length; i++) {
                if (paramlist[i] == msgid) {
                    $.ajax({
                        url: '/AjaxSendMsg/getSendMsggoById.cspx',
                        type: 'POST',
                        data: { 'msgid': msgid, 'MsgIds': hdParamList },
                        success: function (responsevalue) {
                            var svalue = eval(responsevalue);
                            if (svalue == "" || svalue == null) {
                                alert('已经当前页是最后一题了！');
                            } else {
                                if (svalue.MsgId != null) {
                                    formClear();
                                    var ReplyTime = "";
                                    if (svalue.ReplyTime == null || svalue.ReplyTime == "") {

                                    } else {
                                        ReplyTime = ConertJsonTimeAndFormat(svalue.ReplyTime, 'yyyy-MM-dd hh:mm:ss');
                                    }
                                    var QuTime = ConertJsonTimeAndFormat(svalue.QuTime, 'yyyy-MM-dd hh:mm:ss');
                                    $("#lbHardwareVersion").html(svalue.HardwareVersion);
                                    $("#lbSoftwareVersion").html(svalue.SoftwareVersion);
                                    $("#hdMsgId").val(svalue.MsgId);
                                    $("#lbQuTime").html(QuTime);
                                    if (svalue.ImgUrl != '' || svalue.ImgUrl != null) {
                                        $("#ImgUrl").attr('src', 'http://img.upchina.com' + svalue.ImgUrl);
                                    }
                                    $("#lbTypeContent").html(svalue.TypeContent);
                                    $("#lbQuCustomerName").html(svalue.QuCustomerName);
                                    $("#lbMsgContent").html(svalue.MsgContent);
                                    $("#txtReplyContent").html(svalue.ReplyContent);
                                    if (svalue.ReplyContent == null || svalue.ReplyContent == "") {
                                        $("#txtReplyContent").removeAttr("disabled");
                                        $("#btnReply").css("display", "block");
                                    } else {
                                        $("#txtReplyContent").attr("disabled", "disabled");
                                        $("#btnReply").css("display", "none");
                                    }
                                    $("#txtReplier").val(svalue.Replier);
                                    $("#txtReplyTime").val(ReplyTime);
                                } else
                                    $("#btngo").css("display", "none");
                                var tel = (svalue.Tel == '' || svalue.Tel == null || svalue.Tel == undefined) ? '' : (svalue.Tel.substring(0, svalue.Tel.length - 4) + '****');
                                $("#lbTel").html('');
                                $("#lbTel").html(tel);
                                $("#ImgUrl").attr("src", (svalue.ImgUrl == null || svalue.ImgUrl == "" || svalue.ImgUrl == undefined) ? "" : "http://img.upchina.com" + svalue.ImgUrl)
                            }
                        }
                    });
                }
            }
        }

    });
});
function formClear() {
    $("#lbHardwareVersion").html("");
    $("#lbSoftwareVersion").html("");
    $("#lbQuTime").html("");
    $("#lbQuCustomerName").html("");
    $("#lbMsgContent").html("");
    $("#txtReplyContent").html("");
    $("#txtReplier").val("");
    $("#txtReplyTime").val("");
}
//验证方法
function ValidateForm() {
    if (ValidateControl("#txtReplyContent", "回复内容 不能为空。") == false) return false;
    if (ValidateMinAndMax("#txtReplyContent", 10, 500, "回复内容不能小于10个字符或不能大于500个字符。") == false) return false;
    return true;
}