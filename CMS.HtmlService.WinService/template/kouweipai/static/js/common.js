
//捕获页
$(function () {
    document.onkeydown = function (e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13) {
            so();
        }
    }

})
function so1() {
    var keywords = $("#s1").val();
    window.location.href = '/so.html?keywords=' + escape(keywords);
}
function so() {
    var keywords = $("#s").val();
    window.location.href = '/so.html?keywords=' + escape(keywords);
}
//捕获页
function removeHTMLTag(str) {
    if (str == "" || str == null)
        return "";
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    //str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
    str = str.replace(/ /ig, '');//去掉
    return str;
}

function AddFavorite(title, url) {
    try { window.external.addFavorite(url, title); }
    catch (e) {
        try { window.sidebar.addPanel(title, url, ""); }
        catch (e) { alert("您的浏览器不支持JS收藏，请按 Ctrl+D 进行手动收藏！"); }
    }
}

function getLocalTime(nS) {
    var da = nS.replace("/Date(", "").replace(")/", "").substr(0, 10);
    var i = new Date(parseInt(da.substr(0, 10)) * 1000);
    var date = i;
    var year = date.getFullYear();
    var month = date.getMonth() + 1;    //js从0开始取
    var date1 = date.getDate();
    var hour = date.getHours();
    var minutes = date.getMinutes();
    var second = date.getSeconds();
    return year + "-" + (month >= 10 ? month : "0" + month) + "-" + (date1 >= 10 ? date1 : "0" + date1) + " " + (hour >= 10 ? hour : "0" + hour) + ":" + (minutes >= 10 ? minutes : "0" + minutes) + ":" + (second >= 10 ? second : "0" + second);
}

//获取分页数据
function getpagedata(ChannelId, num, rows) {
    var solist = "";
    $.ajax({
        type: "GET",
        url: "/AjaxNews/GetNewsList.cspx?rows=" + rows + "&page=" + num + "&ChannelId=" + ChannelId + "&keywords=",
        success: function (responseText) {
            if (responseText != "") {
                var ctime = "";

                for (var i = 0; i < responseText.rows.length; i++) {

                    if (responseText.rows[i].CreatedTime != "" && responseText.rows[i].CreatedTime != null) {
                        var UploadDate = responseText.rows[i].CreatedTime.replace("/Date(", "").replace(")/", "");
                        ctime = getLocalTime(UploadDate.substr(0, 10));
                    }
                    var strImg = "";
                    if (responseText.rows[i].ImgUrl != "" && responseText.rows[i].ImgUrl != "null" && responseText.rows[i].ImgUrl != null) {
                        if (responseText.rows[i].ImgUrl.toLowerCase().indexOf("http://") === 0) {
                            strImg = ' <div class="focus"><a target="_blank" href="' + responseText.rows[i].Url + '" class="thumbnail"><img  src="' + responseText.rows[i].ImgUrl + '" alt="' + responseText.rows[i].Title + '" /></a></div>';
                        } else {
                            strImg = ' <div class="focus"><a target="_blank" href="' + responseText.rows[i].Url + '" class="thumbnail"><img  src="http://img.pinyifu.com' + responseText.rows[i].ImgUrl + '" alt="' + responseText.rows[i].Title + '" /></a></div>';

                        }
                    }
                    solist += ' <article class="excerpt">' + strImg + '' +
                            '<header><h2><a href="' + responseText.rows[i].Url + '" title="' + responseText.rows[i].Title + '">' + responseText.rows[i].Title + '</a></h2>   </header>' +
                            '<p><span class="muted"><i class="icon-user icon12"></i></span>' +
                            '<span class="muted"><i class="icon-time icon12"></i>  ' + ctime + '</span> </p>' +
                            ' <p class="note">' + removeHTMLTag(responseText.rows[i].Content).substr(0, 150) + '</p> </article>';

                }
            }
            $('#p1').html(solist);

        }
    });

}