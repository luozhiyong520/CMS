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
    myform.action = "/AjaxTest/Upload.cspx";
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