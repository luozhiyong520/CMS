$(function () {
    var type = GetUrlParame("type");
    if (type == 1) {
        $("#aid").html("<a href=\"analystlist.aspx\" class='easyui-linkbutton l-btn l-btn-plain' plain=\"true\" iconCls='icon-add'><span class=\"l-btn-left\"><span class=\"l-btn-text icon-add\" style=\"padding-left: 20px;\">现货分析师列表</span></span></a> ");
        $("#yes").attr("disabled", true);
        $("#no").attr("disabled", true);
        $("#AnalystType").attr("disabled", true);
        $("#VipType").attr("disabled", true);
        $("#SoftVersion").attr("disabled", true);
    } else if (type == 2) {
        $("#aid").html("<a href=\"analysttotallist.aspx\" class='easyui-linkbutton l-btn l-btn-plain' plain=\"true\" iconCls='icon-add'><span class=\"l-btn-left\"><span class=\"l-btn-text icon-add\" style=\"padding-left: 20px;\">分析师列表</span></span></a> ");
    } else if (type == 3) {
        $("#yes").attr("disabled", true);
        $("#no").attr("disabled", true);
        $("#AnalystType").attr("disabled", true);
        $("#VipType").attr("disabled", true);
        $("#SoftVersion").attr("disabled", true);
        $("#aid").html("<a href=\"gjsanalystlist.aspx\" class='easyui-linkbutton l-btn l-btn-plain' plain=\"true\" iconCls='icon-add'><span class=\"l-btn-left\"><span class=\"l-btn-text icon-add\" style=\"padding-left: 20px;\">贵金属分析师列表</span></span></a> ");
    }

    self.parent.document.body.style.overflow = 'hidden';
    var editor = new baidu.editor.ui.Editor({
        UEDITOR_HOME_URL: '/themes/default/scripts/ueditor/',
        autoClearinitialContent: false,
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

    //初始化数据
    LoadData();
    //添加事件
    //    $("#btnEdit").click(function () {
    //        $("#txtFansNum").attr("disabled", false);
    //        $("#tableinfo textarea").attr("disabled", false);
    //        $("#btnSave").css("display", "inline-block");
    //        $("#btnReset").css("display", "inline-block");
    //        $(this).css("display", "none");
    //    });


    //编辑Analyst
    $("#btnEdit").click(function () {
        if (ValidateForm()) {
            var Intro = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
            if (Intro.length < 1) {
                $.messager.alert('消息提示', '简介不能为空。', 'warning');
                return false;
            }
            var str = $("#tableinfo :input").fieldSerialize() + "&Intro=" + Intro;
           
            // var str = ($("#tableinfo :input").fieldSerialize());
            str += "&AnalystId=" + GetUrlParame("AnalystId") + "&AnalystName=" + $("#txtAnalystName").val();
      
            if (type == 1 || type == 3) {
                var AnalystType = GetUrlParame("AnalystType");
                var VipType = $("#VipTypeHidden").val();
                var SoftVersion = $("#SoftVersionHidden").val();
                var AnalystStatus = $("#StatusHidden").val();
                str += "&AnalystType=" + AnalystType + "&AnalystStatus=" + AnalystStatus + "&SoftVersion=" + SoftVersion + "&VipType=" + VipType;

            }
            
            $.ajax({
                url: "/AjaxAnalyst/EditAnalyst.cspx",
                data: str,
                type: "POST",
                success: function (json) {
                    if (json != null) {
                        $.messager.alert('消息提示', '修改成功！', 'info', function () {
                            window.location = window.location;
                        });

                        //                        $("#txtFansNum").attr("disabled", true);
                        //                        $("#tableinfo textarea").attr("disabled", true);
                        //                        $("#btnSave").css("display", "none");
                        //                        $("#btnReset").css("display", "none");
                        //                        $("#btnEdit").css("display", "block");
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
//初始化数据
function LoadData() {
    $.ajax({
        url: "/AjaxAnalyst/AnalystByAnalystId.cspx?AnalystId=" + GetUrlParame("AnalystId"),
        type: "GET",
        async: false,
        success: function (json) {
            if (json == '' || json == null || json == '[object XMLDocument]') {
            } else {
             
                $("#txtAnalystName").val(json.AnalystName);
                $("#txtFansNum").val(json.FansNum);
                $("#AnalystType").val(json.AnalystType);
                $("#txtIntro").val(json.Intro);
                $("#txtNotice").val(json.Notice);
                $("#hdFansNum").val(json.FansNum);
                $("#hdIntro").val(json.Intro);
                $("#hdNotice").val(json.Notice);
                $("#AdminId").val(json.AdminId);
                $("#NickName").val(json.NickName);
                $("#VipType option[value=" + json.VipType + "]").attr("selected", "selected");
                 
                $("#SoftVersion option[value=" + json.SoftVersion + "]").attr("selected", "selected");

                $("#VipTypeHidden").val(json.VipType);
                $("#SoftVersionHidden").val(json.SoftVersion);
                $("#StatusHidden").val(json.AnalystStatus);

                $("#Accuracy").val(json.Accuracy);
                $("#Stability").val(json.Stability);
                $("#Defense").val(json.Defense);
                $("#Attack").val(json.Attack);
                $("#Mentality").val(json.Mentality);

                if (json.AnalystStatus == 1) {
                    $("#no").attr("checked", "");
                    $("#yes").attr("checked", "checked");
                } else {
                    $("#yes").attr("checked", "");
                    $("#no").attr("checked", "checked");
                }
                $("#imagesrc").html('');
                $("#imagesrc").append("<img src=" + json.ImgUrl + " width=80px height=80px/><input type='hidden' value=" + json.ImgUrl + " name='ImgUrl'/> ");
            }
        }
    });
}
//验证方法 
function ValidateForm() {
    if (ValidateControl("#txtAnalystName", "名称 不能为空。") == false) return false;
    if (ValidateControl("#txtFansNum", "粉丝数 不能为空。") == false) return false;
    if (ValidateControl("#AnalystType", "直播类型 不能为空。") == false) return false;
    //if (ValidateControl("#txtIntro", "简介 不能为空。") == false) return false;
    if ($("#imagesrc input[type='hidden']").val() == null || $("#imagesrc input[type='hidden']").val() == "" || $("#imagesrc input[type='hidden']").val()==undefined) {
        $.messager.alert('消息提示', '上传头像 不能为空。', 'warning');
        return false;
    }
    return true;
}

function GetUrlParame(parame) {
    var reg = new RegExp("(^|&)" + parame + "=([^&]*)(&|$)", "i");
    var res = "";
    try {
        res = window.location.search.substr(1).match(reg)[2];
    } catch (e) { }
    return res;
}

