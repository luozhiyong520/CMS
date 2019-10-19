$(function () {
    $("#handnews").click(function () {
        $("#newsabstract").css("display", "");
    })
    $("#autonews").click(function () {
        $("#newsabstract").css("display", "none");
    })
    $("#localpic").click(function () {
        if (window.ActiveXObject) { //IE

            var returnValue = window.showModalDialog("/media/imagelist.aspx", window, "dialogWidth:900px;status:no;dialogHeight:640px");
            if (returnValue != null) {
                setValue(returnValue.name);
            }

        } else {  //非IE
            
            window.open("/media/imagelist.aspx", 'newwindow', 'height=640,width=900,top=150,left=300,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');

        }
    })



    $("#addArticle").click(function () {
        $("#addArticle").attr("disabled", true);
        var str = $("#newsinfo :input").fieldSerialize();
        var content = encodeURIComponent(editor.getContent().replace(new RegExp("&#39;", 'gm'), ""));
 
        
        str = str + "&Content=" + content;
         
        if ($("#title").val() == "") {
            alert("标题不能为空");
            $("#addArticle").attr("disabled", false);
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            $("#addArticle").attr("disabled", false);
            return;
        }
        else if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
            $("#addArticle").attr("disabled", false);
            return;
        } else if (editor.getContent() == "") {
            alert("内容不能为空");
            $("#addArticle").attr("disabled", false);
            return;
        } else {
            $.ajax({
                url: "/AjaxArticle/AddEditArticle.cspx",
                data: str,
                type: "POST",
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                        $("#addArticle").attr("disabled", false);
                    } else if (responseText == "000002") {
                        alert("请选择一个分类");
                        $("#addArticle").attr("disabled", false);
                    } else if (responseText == "000003") {
                        alert("内容不能为空");
                        $("#addArticle").attr("disabled", false);
                    } else if (responseText == "000004") {
                        alert("来源不能为空");
                        $("#addArticle").attr("disabled", false);
                    }
                    else {
                        alert("添加成功");
                        $("#addArticle").attr("disabled", false);
                        window.location = window.location;
                    }
                }
            })
        }
    })

    $("#addemptynews").click(function () {
        var check = CheckInput();
        if (check == false)
            return;
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
                        alert("添加成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })

    $("#addpicnews").click(function () {

        var str = decodeURI($("#picnews :input").fieldSerialize());
 
        if ($("#title").val() == "") {
            alert("标题不能为空");
            return;
        } else if ($("#url").val() == "") {
            alert("文章url不能为空");
            return;
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        }
        else if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
            return;
        } else {

            $.ajax({
                url: "/AjaxArticle/AddEditPicArticle.cspx?strurl="+url,
                data: str,
                type: "POST",
                complete: function () { },
                success: function (responseText) {
                    if (responseText == "000001") {
                        alert("标题不能为空");
                    } else if (responseText == "000002") {
                        alert("文章url不能为空");
                    } else if (responseText == "000003") {
                        alert("请选择一个分类");
                    } else if (responseText == "000004") {
                        alert("图片不能为空");
                    } else {
                        alert("添加成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })

    $("#addTitleArticle").click(function () {
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
        } else if ($("#CreatedTime").val() == "") {
            alert("发布时间不能为空");
            return;
        } else if ($("#ChannelId").val() == 0) {
            alert("请选择一个分类");
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
                        alert("请选择一个分类");
                    } else {
                        alert("添加成功");
                        window.location = window.location;
                    }
                }
            })
        }
    })




});


function setValue(returnValue) {
    $("#ImgUrl").val(returnValue);
}


