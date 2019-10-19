$(function () {
    $("#handnews").click(function () {
        $("#newsabstract").css("display", "");
    })
    $("#autonews").click(function () {
        $("#newsabstract").css("display", "none");
    })
    $("#localpic").click(function () {
        var returnValue = window.showModalDialog("/media/imagelist.aspx", 'newwindow', "dialogHeight=640px;dialogWidth=900px;dialogLeft=260px;dialogTop=100px");
        if (returnValue != undefined) {
            $("#ImgUrl").val(returnValue);
        }

    })

    var newsId = $.getUrlVar("newsId");

    $("#editArticle").click(function () {
        $("#editArticle").attr("disabled", true);
        var str = $("#newsinfo :input").fieldSerialize();
        var content = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
   
        str = str + "&Content=" + content;
        if ($("#title").val() == "") {
            alert("标题不能为空");
            return;
        } else if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
            return;
        } else if ($("#Author").val() == "") {
            alert("来源不能为空");
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        } else if (editor.getContent() == "") {
            alert("内容不能为空");
            return;
        } else {
            $.ajax({
                type: "POST",
                url: "/AjaxArticle/AddEditArticle.cspx",
                data: str,
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                        $("#editArticle").attr("disabled", false);
                    } else if (responseText == "000002") {
                        alert("分类有误");
                        $("#editArticle").attr("disabled", false);
                    } else if (responseText == "000003") {
                        alert("内容不能为空");
                        $("#editArticle").attr("disabled", false);
                    } else if (responseText == "000004") {
                        alert("来源不能为空");
                        $("#editArticle").attr("disabled", false);
                    }
                    else {
                        alert("修改成功");
                        $("#editArticle").attr("disabled", false);
                        window.location = window.location;
                    }
                }
            });
        }
    })


    $("#editEmptyArticle").click(function () {
        CheckInput();
        var str = decodeURI($("#emptynews :input").fieldSerialize());
        var SecondTitle = $("#hiddenTitle").val();
        var SecondUrl = $("#hiddentUrl").val();
        var TitleColor = $("#hiddenColor").val();
        var IsBold = $("#hiddenJC").val();

        str = str + "&SecondTitle=" + SecondTitle + "&SecondUrl=" + SecondUrl + "&TitleColor=" + TitleColor + "&IsBold=" + IsBold;
        if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        } else {
            $.ajax({
                url: "/AjaxArticle/AddEditEmptyArticle.cspx",
                data: str,
                type: "POST",
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                    } else if (responseText == "000002") {
                        alert("请选择一个分类");
                    } else {
                        alert("修改成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })


    $("#editPicArticle").click(function () {
        var str = decodeURI($("#picnews :input").fieldSerialize());
        if ($("#title").val() == "") {
            alert("标题不能为空");
            return;
        } else if ($("#url").val() == "") {
            alert("文章url不能为空");
            return;
        }
        else if ($("#ChannelId").val() ==0) {
            alert("请选择一个分类");
            return;
        }  else if ($("#ImgUrl").val() == "") {
            alert("图片不能为空");
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        } else {
            $.ajax({
                url: "/AjaxArticle/AddEditPicArticle.cspx",
                data: str,
                type: "POST",
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                    } else if (responseText == "000002") {
                        alert("文章url不能为空");
                    } else if (responseText == "000003") {
                        alert("分类有误");
                    } else if (responseText == "000004") {
                        alert("图片不能为空");
                    }else {
                        alert("修改成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })

    $("#editTitleArticle").click(function () {
        var stockcode = "";
        for (var i = 0; i < $("#taglist li").length; i++) {
            if (i == $("#taglist li").length - 1)
                stockcode += $("#taglist li").eq(i).children().html();
            else
                stockcode += $("#taglist li").eq(i).children().html() + ",";
        }

        var str = decodeURI($("#titleinfo :input").fieldSerialize()) + "&stockcode=" + stockcode;
   
        if ($("#title").val() == "") {
            alert("标题不能为空");
            return;
        } else if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        } else {
            $.ajax({
                url: "/AjaxArticle/AddEditTitleArticle.cspx",
                data: str,
                type: "POST",
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                    } else if (responseText == "000002") {
                        alert("分类有误");
                    }else {
                        alert("修改成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })

});



function stripscript(s) {
    var pattern = new RegExp("&#39;", 'gm');
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '');
    }
    return rs;
} 
