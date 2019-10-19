$(function () {
    $("#inputNum").html($("#title").val().length);
});
var g_imageObject, g_ColorValue;

var title = "";
var url = "";
var ZT = "";
var color = "";
function AddOtherTitles(newsId, titlestr, urlstr, isBlodstr, titleColorstr, channelId, obj) {
 
    var object = document.getElementById("AddOtherTitles");
    var titles = titlestr.split("[WL]");
    var urls = urlstr.split("[WL]");
    var isBlods = isBlodstr.split("[WL]");
    var titleColors = titleColorstr.split("[WL]");

    if ($("#AddOtherTitles").css("display") == "block") {
        HideAddTitles();
    }
    else {
 
        $("#AddOtherTitles").css("display", "");
        $("#AddOtherTitles").css("position", "absolute");

        //$("#AddOtherTitles").css("top", getPosition(obj).y + obj.offsetHeight + 7);
        //$("#AddOtherTitles").css("left", obj.clientLeft + 55);

        $("#AddOtherTitles").css("top", getHeight() / 2);
        $("#AddOtherTitles").css("left", self.parent.document.body.clientWidth / 2 - 360);


        
        $("#AddOtherTitles").css("zIndex", "10000");

        $("#hiddenNewsId").val(newsId);
        $("#hiddenChannelId").val(channelId);

        for (var i = 0; i < titles.length; i++) {
            title = titles[i];
            url = urls[i];
            ZT = isBlods[i];
            editline();
            g_imageObject = document.getElementById("imgColor" + i);
            g_ColorValue = document.getElementById("txtColorValue" + i);
            setColor(titleColors[i]);
        }
  
    }
}

 

 


function HideAddTitles() {
    var listtable = document.getElementById('listtable');
    while (listtable.rows.length > 1) {
        listtable.deleteRow(1);
    }
    allCount = 1;
    var object = document.getElementById("AddOtherTitles");
    object.style.display = "none";
}


var allCount = 0;
//增加一行
function addline() {
    allCount = $("#listtable tr").length-1;
    var check = CheckInput();

    if (check == false)
        return;

    var listtable = document.getElementById('listtable');
    newRow = listtable.insertRow(listtable.rows.length);
    newRow.ln = allCount;
    newRow.id = allCount;
    var index = allCount % 2;

    if (index == 0) {
        newRow.bgColor = "#DFEEFF";
    }
    else {
        newRow.bgColor = "#FFFFFF";
    }
    newRow.align = "center";
    c1 = newRow.insertCell(0);
    c1.innerHTML = "<input type='text' size='10' name='title' class='inTextbox' style='width:400px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;' id='title" + allCount + "'>";

    c2 = newRow.insertCell(1);
    var txtColorValueId = "txtColorValue" + allCount;
    c2.innerHTML = "<input type='text' size='10' name='url' style='width:260px; ' class='inTextbox' value='http://'><input  type='hidden' value=''  name='color' id='" + txtColorValueId + "'/>";

    c3 = newRow.insertCell(2);
    var imgid = "imgColor" + allCount;
    c3.innerHTML = "<img src='/themes/default/images/admin/Gcolor.gif' alt='标题颜色选取' id='" + imgid + "' onclick=getColor(this,'" + txtColorValueId + "') width='18' height='17' border='0' align='middle' style='cursor: pointer;background-color: #' />";
    c4 = newRow.insertCell(3);
    c4.innerHTML = "<input type='checkbox'   name='JC'>";

    c5 = newRow.insertCell(4);
    c5.id = "line" + allCount;
    c5.innerHTML = "<input type='button' value='删除' class='inTextbox' onclick=delline(" + allCount + ")>";
    allCount++;

    c6 = newRow.insertCell(5);
    c6.id = "line" + allCount;
    c6.innerHTML = "<input type='button' value='向上移动' onclick=\"moveline(this)\">";
}
 

//获取颜色
function getvalue(img, hiddenid, txtvalueid) {
    var paletteLeft, paletteTop
    var colorPalette = document.getElementById("frmColorPalette");
   
    g_ColorValue = document.getElementById(txtvalueid);
    g_imageObject = img;
 
    if (colorPalette) {
        paletteLeft = getOffsetLeft(g_imageObject);
        paletteTop = (getOffsetTop(g_imageObject) - 180);

        if (paletteTop < 0) {
            paletteTop += g_imageObject.offsetHeight + 165;
        }
        if (paletteLeft + 260 > parseInt(document.body.clientWidth)) {
            paletteLeft = parseInt(event.clientX) - 280;
        }
        colorPalette.style.left = paletteLeft + "px";
        colorPalette.style.top = paletteTop + 20 + "px";

        if (colorPalette.style.visibility == "hidden") {
            colorPalette.style.visibility = "visible";
        } else {
            colorPalette.style.visibility = "hidden";
        }
    }
}

 
//删除一行
function delline(lineid) {
    var listtable = document.getElementById('listtable');
    var row = document.getElementById(lineid);
    var rowindex = row.rowIndex;
    listtable.deleteRow(rowindex);
    allCount--;
    return;
}

//移动一行  只适用IE浏览器
//function moveline(lineid) {
//    //var $obj1 = $(lineid).parent().parent("tr").attr('id');
//    var _row = lineid.parentNode.parentNode;
//    var tdSke = _row.rowIndex;
//    //如果不是第一行，实现两行互换
//    if (tdSke != 1) {
//        _row.previousSibling.swapNode(_row, _row.previousSibling);
//    } else {
//        alert("已经到顶部!");
//    }
//}

//移动一行 
function moveline(lineid) {
    var _row = $(lineid).parent().parent();
    var prev = _row.prev();
    if (_row.index() > 1) {
        _row.insertBefore(prev);
    } else {
        alert("已经到顶部!");
    }
}

//检查输入
function CheckInput() {
    var titles = document.getElementsByName("title");
    var urls = document.getElementsByName("url");
    var colors = document.getElementsByName("color");
    var JCs = document.getElementsByName("JC");
    var titlestr = "";
    var urlstr = "";
    var colorstr = "";
    var ZTstr = "";

    for (var i = 0; i < titles.length; i++) {
        if (titles[i].value == '') {
            alert("标题不能为空！");
            titles[i].focus();
            return false;
        }
        else {
            if (i == titles.length - 1)
                titlestr += titles[i].value;
            else
                titlestr += titles[i].value + "[WL]";
        }
    }
    document.getElementById("hiddenTitle").value = titlestr;
    for (var i = 0; i < urls.length; i++) {
        var url;
        if (urls[i].value == '') {
            urls[i].focus();
            alert("链接不能为空！");
            return false;
        }
        else {

            if (urls[i].value.substring(0, 4) == ("http").toLowerCase() || urls[i].value.substring(0, 5) == ("https").toLowerCase()) {
                if (i == urls.length - 1)
                    urlstr += urls[i].value;
                else
                    urlstr += urls[i].value + "[WL]";

            } else {
                url = "http://" + urls[i].value;
                if (i == urls.length - 1)
                    urlstr += url;
                else
                    urlstr += url + "[WL]";
            }

        }
    }
    document.getElementById("hiddentUrl").value = urlstr;

    for (var i = 0; i < colors.length; i++) {
        if (i == colors.length - 1)
            colorstr += colors[i].value;
        else
            colorstr += colors[i].value + "[WL]";
    }
    document.getElementById("hiddenColor").value = colorstr;
  
    for (var i = 0; i < JCs.length; i++) {
        if (JCs[i].checked == true) {
            if (i == JCs.length - 1)
                ZTstr += "1";
            else
                ZTstr += "1[WL]";
        }
        else {
            if (i == JCs.length - 1)
                ZTstr += "0";
            else
                ZTstr += "0[WL]";
        }
    }
    document.getElementById("hiddenJC").value = ZTstr;
    return true;
}

///编辑     
function editline() {
    allCount = $("#listtable tr").length - 1;
    var listtable = document.getElementById('listtable');
    newRow = listtable.insertRow(listtable.rows.length);
    newRow.ln = allCount;
    newRow.id = allCount;
    var index = allCount % 2;
    if (index == 0) {
        newRow.bgColor = "#DFEEFF";
    }
    else {
        newRow.bgColor = "#FFFFFF";
    }
    newRow.align = "center";
    c1 = newRow.insertCell(0);
    c1.innerHTML = "<input type='text' name='title' class='inTextbox' value='" + title + "' style='width:400px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;'>";

    c2 = newRow.insertCell(1);
    var txtColorValueId = "txtColorValue" + allCount;
    c2.innerHTML = "<input type='text' size=10 name='url' value='" + url + "' style='width:260px;' class='inTextbox'><input id='" + txtColorValueId + "' type='hidden' value='FFFFFF'  name='color'/>";

    c3 = newRow.insertCell(2);
    var imgid = "imgColor" + allCount;
    c3.innerHTML = "<img src='/themes/default/images/admin/Gcolor.gif' id='" + imgid + "' alt='标题颜色选取' onclick=getColor(this,'" + txtColorValueId + "') width='18' height='17' border='0' align='middle' style='cursor: pointer;background-color: #' />";

    c4 = newRow.insertCell(3);
    if (ZT == 1) {
        c4.innerHTML = "<input type='checkbox' checked='checked'  name='JC'>";
    }
    else {
        c4.innerHTML = "<input type='checkbox'  name='JC'>";
    }

    c5 = newRow.insertCell(4);
    c5.id = "line" + allCount;
    c5.innerHTML = "<input type='button' value='删除' class='inTextbox' onclick=delline(" + allCount + ")>";
    allCount++;

    c6 = newRow.insertCell(5);
    c6.id = "line" + allCount;
    c6.innerHTML = "<input type='button' value='向上移动' id='bntChange' onclick=\"moveline(this)\">";
}

//function move() {
//    if (allCount != 1) {
//        c6 = newRow.insertCell(5);
//        c6.id = "line" + allCount;
//        c6.innerHTML = "<input type='button' value='向上移动' onclick=\"moveline(this)\">";
//    } else {
//        c6 = newRow.insertCell(5);
//        c6.id = "line" + allCount;
//        c6.innerHTML = "<input type='button' value='顶部' disabled>";
//    }
//}

function getColor(imageObject, txtColorValueId) {

    var paletteLeft, paletteTop
    var colorPalette = document.getElementById("frmColorPalette");
    g_ColorValue = document.getElementById(txtColorValueId);
    g_imageObject = imageObject;

    if (colorPalette) {

        paletteLeft = getOffsetLeft(g_imageObject);
        paletteTop = (getOffsetTop(g_imageObject) - 180);

        if (paletteTop < 0) {
            paletteTop += g_imageObject.offsetHeight + 165;
        }
        if (paletteLeft + 260 > parseInt(document.body.clientWidth)) {
            paletteLeft = parseInt(event.clientX) - 280;
        }
        colorPalette.style.left = paletteLeft + "px";
        colorPalette.style.top = paletteTop + 20 + "px";

        if (colorPalette.style.visibility == "hidden") {
            colorPalette.style.visibility = "visible";
        } else {
            colorPalette.style.visibility = "hidden";
        }
    }
}

function getOffsetLeft(elm) {
    var mOffsetLeft = elm.offsetLeft;
    var mOffsetParent = elm.offsetParent;
    while (mOffsetParent) {
        mOffsetLeft += mOffsetParent.offsetLeft;
        mOffsetParent = mOffsetParent.offsetParent;
    }
    return mOffsetLeft;
}

function getOffsetTop(elm) {
    var mOffsetTop = elm.offsetTop;
    var mOffsetParent = elm.offsetParent;
    while (mOffsetParent) {
        mOffsetTop += mOffsetParent.offsetTop;
        mOffsetParent = mOffsetParent.offsetParent;
    }
    return mOffsetTop;
}


function setColor(color) {

    if (g_imageObject.id == "FontColorShow" && color == "#") color = '#000000';
    if (g_imageObject.id == "FontBgColorShow" && color == "#") color = '#FFFFFF';
    if (g_ColorValue) {
        g_ColorValue.value = color;
    }

    if (g_imageObject && color.length > 1) {
        g_imageObject.src = src = '/themes/default/images/admin/Gcolor.gif';
        g_imageObject.style.backgroundColor = color;
    } else if (color == '#') {
        g_imageObject.src = '';
        g_imageObject.style.backgroundColor = '';
        g_imageObject.src = '/themes/default/images/admin/Rect2.gif';
    }
    document.getElementById("frmColorPalette").style.visibility = "hidden";

}

function changebold(title, strong) {
    var input = $("#" + title + "");
    if ($("#" + strong + "").attr("checked") == "checked") {
        input.css("font-weight", "700");
    }
    else {
        input.css("font-weight", "normal");
    }
}

function getInputNum() {
    var title = $("#title").val();
    for (var i = 0, num = 0; i < title.length; i++) {
        var charcode = title.charCodeAt(i);
        if (charcode > 128) {
            num += 2;
        } else {
            num++;
        }
    }
    $("#inputNum").html(num / 2);
    //  $("#inputNum").html($("#txtTitle").val().length);
}