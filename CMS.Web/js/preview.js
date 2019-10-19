function makeListV2addspecid(h) {
    var specids = "";
    var idn = "makelistv2specids";
    specids = document.getElementById(idn).value;
    specids = specids + h + ",";
    document.getElementById(idn).value = specids;
}

//n为1是预览时在新窗口打开。
function makeListV2editspec(h, n) {
    var url = "ff/editspecwin.jsp?specid=" + h + "&t=" + Math.random() + "&n=" + n + "&locurl=" + encodeURIComponent(document.location.href);
    //var url = "http://cms.upchina.com/UI/spec/editspecwin.jsp?specid="+h;
    //if(navigator.userAgent.indexOf('Firefox') >= 0){
    //url="ff/previewedit.jsp?specid="+h+"&t="+Math.random()+"&n="+n+"&locurl="+encodeURIComponent(document.location.href);
    //}
    var sFeatures = "dialogWidth=736px;dialogHeight=530px;dialogLeft=100;dialogTop=100;resizable=1;scroll=0";
    var returnValue = window.showModalDialog(url, null, sFeatures);
    if (returnValue != null && returnValue != 'undefined') {
        var st = returnValue.indexOf("|");
        if (returnValue.substring(0, st) == "预览") {
            makeListV2addspecid(h);
        }
        var s1 = returnValue.substring(st + 1);
        var st1 = s1.indexOf("|");
        var s2 = s1.substring(0, st1);
        document.getElementById("makelistv2inputman").value = s2;
        document.getElementById("makelistv2peripheral" + h).innerHTML = s1.substring(st1 + 1);
    }
}

function makeListV2SetBG1(o, p) {
    o.style.background = "#D4D0C8";
    o.style.position = "relative";
    var m = document.getElementById(p);
    m.style.display = "block";
    m.style.background = "#fff000 url(themes/default/images/editioc.gif)";
    m.style.top = 0 + "px";
    m.style.right = 0 + "px";
    m.style.width = 45 + "px";
    m.style.height = 25 + "px";
    m.style.position = "absolute";
    m.style.cursor = "pointer";
    m.style.zIndex = "999";
}
function makeListV2SetBG2(o, p) {
    o.style.background = "";
    var m = document.getElementById(p);
    m.style.display = "none";
}