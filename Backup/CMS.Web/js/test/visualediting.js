$(function () {
    loadClick();

})

//添加事件
function loadClick() {
    $("div[deit]").each(function () {

        $(this).append("<input sub=\"sub\" type=\"button\" value=\"提 交\" onclick=\"sub(this)\" style=\"display: none;width:150px;height:50px;\"/>");

        $(this).mouseover(function () {
            $(this).children("input[sub = 'sub']").show();
        }).mouseout(function () {
            $("input[sub = 'sub']").hide();
        });

        $(this).find("a").each(function () {
            $(this).attr("url", $(this).attr("href"));
            $(this).removeAttr("href");
        });

        var as = $(this).find("a:not(:has(img))");
        as.each(function () {

            $(this).click(function () {
                //alert($(this).html());
                $("#cancel").click();

                //if (IsEdit(this)) return;

                var txtVal = $(this).html();
                $(this).attr("text", txtVal);   //取消时,用于还原值
                $(this).html("<input type=\"text\" id=\"txtTitle\"/>");
                $(this).unbind("click");
                $("#txtTitle").val(txtVal).focus().select();
                openEdit(this);

            });

            $(this).dblclick(function () {
                $(this).attr("editid","adda");
                closeNewConsultingA();
                openAddA(this);
            });

        });
        var imgs = $(this).find("img");
        imgs.each(function () {
            $(this).click(function () {
                //alert("img");
                //                var ab = $(this).parent("a");
                //                alert(ab);
                //                alert(ab.attr("url"));
                $(this).attr("editid", "editimg");
                openEditImg(this);
            });
        });
    });
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

//弹出添加A标签层
function openAddA(obj) {
    pos = GetObjPos(obj);
    var thisEditA = $("#txtTitle").parent();
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
    newDiv.style.zIndex = "9999";
    newDiv.style.width = "158px";
    newDiv.style.height = "25px";
    newDiv.style.top = (pos.y + 6) + "px";
    newDiv.style.left = divLeft + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "3px";
    newDiv.innerHTML =
        "<div style=\"padding:0 0 0 0;width:100%;\">" +
                "<input type=\"button\" value=\"增 加\" onclick=\"addA()\"/><input type=\"button\" value=\"删 除\" onclick=\"delA()\"/><input type=\"button\" value=\"取 消\" onclick=\"closeNewConsulting()\"/>" +
        "</div>";
    document.body.appendChild(newDiv);
}

//弹出A标签编辑层
function openEdit(obj) {
    pos = GetObjPos(obj);
    var thisEditA = $("#txtTitle").parent();
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
    newDiv.style.zIndex = "9999";
    newDiv.style.width = "343px";
    newDiv.style.height = "60px";
    newDiv.style.top = (pos.y + 12) + "px";
    newDiv.style.left = divLeft + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "5px";
    newDiv.innerHTML =
        "<div style=\"padding:0 0 0 0;width:100%;\">" +
            "<div style=\"width:100%;float:left;\">" +
                "<div style=\"float:left;padding:7px 0 0 0;\">Url:</div>" +
                "<div style=\"float:right;padding:1px 0 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"txtUrl\" /><input type=\"button\" value=\"确 定\" style=\"width:80px;\" onclick=\"updateA()\"/></div>" +
            "</div>" +
            "<div style=\"width:100%;float:left;\">" +
                "<div style=\"float:left;padding:9px 0 0 0;\">Title:</div>" +
                "<div style=\"float:right;padding:3px 0 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"txtTitlePro\" /><input id=\"cancel\" type=\"button\" value=\"取 消\" style=\"width:80px;\" onclick=\"closeNewConsultingA()\"/></div>" +
            "</div>" +
        "</div>";
    document.body.appendChild(newDiv);

    //    alert(thisEditA.arrt("url"));
    //    alert(thisEditA.arrt("title"));
    $("#txtUrl").val(thisEditA.attr("url"));
    $("#txtTitlePro").val(thisEditA.attr("title"));
}

//弹出img图片标签编辑层
function openEditImg(obj) {
    pos = GetObjPos(obj);
    var markID = $(obj).attr("mark");
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
    newDiv.style.zIndex = "9999";
    newDiv.style.width = "327px";
    newDiv.style.height = "85px";
    newDiv.style.top = pos.y + "px";
    newDiv.style.left = divLeft + "px";
    newDiv.style.background = "#EFEFEF";
    newDiv.style.border = "1px solid #888888";
    newDiv.style.padding = "5px";
    newDiv.innerHTML =
        "<div style=\"padding:0 0 0 0;width:100%;\">" +
            "<div><span><input type=\"file\" name=\"file\" id=\"upfile\" /></span></div>" +
            "<div style=\"width:100%;float:left;\">" +
                "<div style=\"float:left;padding:1px 0 0 0;\">Url:</div>" +
                "<div style=\"float:right;padding:1px 0 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"txtUrl\" /><input type=\"button\" value=\"确 定\" style=\"width:80px;\" onclick=\"updateImg()\"/></div>" +
            "</div>" +
            "<div style=\"width:100%;float:left;\">" +
                "<div style=\"float:left;padding:3px 0 0 0;\">Alt:</div>" +
                "<div style=\"float:right;padding:3px 0 0 0;\"><input type=\"text\" style=\"width:200px;\" id=\"txtAlt\" /><input id=\"cancel\" type=\"button\" value=\"取 消\" style=\"width:80px;\" onclick=\"closeNewConsulting()\"/></div>" +
            "</div>" +
        "</div>";
    document.body.appendChild(newDiv);
    $("#txtUrl").removeAttr("disabled");
    var thisEditImg = $("img[editid='editimg']");
    var thisEditA = thisEditImg.parent();
    if (thisEditA.attr("url") == undefined)
        $("#txtUrl").attr("disabled", "disabled");
    else 
        $("#txtUrl").val(thisEditA.attr("url"));

    $("#txtAlt").val(thisEditImg.attr("alt"));

}

//关闭A标签编辑弹出层
function closeNewConsultingA() {
    //var thisEditA = $("a[mark='" + markID + "']");
    var thisEditA = $("#txtTitle").parent();

    thisEditA.html(thisEditA.attr("text"));
    thisEditA.removeAttr("text");

    thisEditA.click(function () {
        //if (IsEdit(this)) return;
        $("#cancel").click();

        var txtVal = $(this).html();
        $(this).attr("text", txtVal);
        $(this).html("<input type=\"text\" id=\"txtTitle\"/>");
        $(this).unbind("click");
        $("#txtTitle").val(txtVal).focus().select();
        openEdit(this);
    });

    closeNewConsulting();
}

//关闭编辑弹出层
function closeNewConsulting() {
    document.body.removeChild(docEle("EditForm"));
    $("img[editid='editimg']").removeAttr("editid");
}

//更新A标签
function updateA() {
    //var thisEditA = $("a[mark='" + markID + "']");
    var thisEditA = $("#txtTitle").parent();

    if ($("#txtUrl").val() == "")
        thisEditA.attr("url", "#");
    else
        thisEditA.attr("url", $("#txtUrl").val());

    if ($("#txtTitlePro").val() == "" || $("#txtTitlePro").val() == undefined)
        thisEditA.removeAttr("title");
    else
        thisEditA.attr("title", $("#txtTitlePro").val());

    //thisEditA.attr("text", $("#txtTitle").val());
    thisEditA.removeAttr("text");
    thisEditA.html($("#txtTitle").val() == "" ? "请编辑" : $("#txtTitle").val());

//    if ($("#isNewPage").attr("checked"))
//        thisEditA.attr("target", "_blank");
//    else
//        thisEditA.removeAttr("target");

    //setEditValue(markID);

    thisEditA.click(function () {
        //if (IsEdit(this)) return;

        //var mark = $(this).attr("mark");
        var maxlength = $(this).attr("max") == undefined ? "" : "maxlength=" + $(this).attr("max");

        var txtVal = $(this).html();
        $(this).attr("text", txtVal);
        $(this).html("<input type=\"text\" id=\"txtTitle\" " + maxlength + "/>");
        $(this).unbind("click");
        $("#txtTitle").val(txtVal).focus().select();
        openEdit(this);
    });
    //alert($("#txtUrl").val());
    closeNewConsulting();
}

//更新图片标签
function updateImg() {
    var thisEditImg = $("img[editid='editimg']");
    var thisEditA = thisEditImg.parent();
    var thisEditImgSrc = thisEditImg.attr("src");

    if ($("#upfile").val() == "")
        updateImgRelated("");
    else
        uploadfile();

}

//更新图片相关内容
function updateImgRelated(imgSrc) {
    var thisEditImg = $("img[editid='editimg']");
    var thisEditA = thisEditImg.parent();

    if (thisEditA.attr("url") != undefined) {
        if ($("#txtUrl").val() == "")
            thisEditA.removeAttr("url");
        else
            thisEditA.attr("url", $("#txtUrl").val());
    }

    if ($("#txtAlt").val() == "" || $("#txtAlt").val() == undefined)
        thisEditImg.removeAttr("alt");
    else
        thisEditImg.attr("alt", $("#txtAlt").val());

    if (imgSrc != "")
        thisEditImg.attr("src", imgSrc);

    thisEditImg.removeAttr("editid");

    closeNewConsulting();
}

//增加一个A标签
function addA() {
    var thisEditA = $("a[editid='adda']");
    thisEditA.after("&nbsp;&nbsp;<a id=\"newA\" url=\"#\" target=\"_blank\">请编辑</a>");
    $("#newA").click(function () {
        $("#cancel").click();

        var txtVal = $(this).html();
        $(this).attr("text", txtVal);
        $(this).html("<input type=\"text\" id=\"txtTitle\"/>");
        $(this).unbind("click");
        $("#txtTitle").val(txtVal).focus().select();
        openEdit(this);
    }).dblclick(function () {
        $(this).attr("editid", "adda");
        closeNewConsultingA();
        openAddA(this);
    });
    thisEditA.removeAttr("editid")
    $("#newA").removeAttr("id");
    closeNewConsulting();
}

//删除一个A标签
function delA() {
    var thisEditA = $("a[editid='adda']");
    var thisEditAp = thisEditA.parent();
    
    //同级下, A标签数量
    var i = 0;
    thisEditAp.children("a:not(:has(img))").each(function () {
        i++;
    });
    if (i > 1)
        thisEditA.remove();
    else
        alert("至少保留一条标题!");

    thisEditAp.html(thisEditAp.html().replace("&nbsp;&nbsp;&nbsp;&nbsp;", "&nbsp;&nbsp;"));
    //去除所有提交按钮, 因为div下内容作了替换, 事件消失, 需重新加载事件, 不去除会出现多个按钮
    $("input[sub = 'sub']").remove();
    loadClick();
    closeNewConsulting();
}

//提交
function sub(o) {
    var pDiv = $(o).parent()
    pDiv.find("a").each(function () {
        $(this).attr("href", $(this).attr("url"));
        $(this).removeAttr("url");
    });
    //去除提交按钮
    pDiv.children("input[sub = 'sub']").remove();
    alert(pDiv.html());
    //Insertfragment(pDiv);
    pDiv.find("a").each(function () {
        $(this).attr("url", $(this).attr("href"));
        $(this).removeAttr("href");
    });
    pDiv.append("<input sub=\"sub\" type=\"button\" value=\"提 交\" onclick=\"sub(this)\" style=\"display: none;width:150px;height:50px;\"/>");
}

//新增碎片
function Insertfragment(o) {
    var pDiv = $(o);
    var channelID = pDiv.attr("channelID");
    var content = pDiv.html();
    $.ajax({
        url: "/AjaxFragment/InsertFragment.cspx",
        type: "POST",
        data: { 'channelID': channelID, 'content': content },
        success: function (responseText) {
            alert("操作成功!");
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
            form.remove();
            updateImgRelated(data);
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

function a() {

}
