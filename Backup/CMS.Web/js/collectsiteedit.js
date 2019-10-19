$(function () {
    var siteId = $("#HidSiteId").val();
    if (siteId == "" || siteId == null) {
        $(".lm_div").html("");
        $(".lm_div").append('添加采集站点');
    } else {
        $(".lm_div").html("");
        $(".lm_div").append('编辑采集站点');
    }
    //保存
    $('#BtnSave').click(function(){
        if (siteId == "" || siteId == null) {
            IsnertCollectSite();
        } else {
            ShowCollectSite(siteId);
        }
    });
});
//添加采集站点
function IsnertCollectSite() {
    if (ValidateForm() == false) return;
    var siteName = $('#SiteName').val();
    var siteFolderIds = $('#drpSiteFolderId option:selected').val();
    var siteFolderId = siteFolderIds.split('&')[0];
    var channelId = siteFolderIds.split('&')[1];
    var siteURL = $('#SiteURL').val();
    var encode = $('#Encode').val();
    var audit = $('#drpAudit option:selected').val();
    //采集参数
    var savePic = $('#chSavePic').val();
    var reverse = $('#chReverse').val();
    var picNews = $('#chPicNews').val();
    //过滤选项
    var html = $('#ChbHTML').val();
    var style = $('#ChbSTYLE').val();
    var div = $('#ChbDIV').val();
    var a = $('#ChbA').val();
    var classes = $('#ChbCLASS').val();
    var font = $('#ChbFONT').val();
    var span = $('#ChbSPAN').val();
    var object = $('#ChbOBJECT').val();
    var iframe = $('#ChbIFRAME').val();
    var script = $('#ChbSCRIPT').val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: '/AjaxCollectSite/InsertCollectSite.cspx',
        type: 'POST',
        data: { 'siteName': siteName,
            'siteFolderId': siteFolderId,
            'siteURL': siteURL,
            'encode': encode,
            'channelId': channelId,
            'audit': audit,
            'savePic': savePic,
            'reverse': reverse,
            'picNews': picNews,
            'html': html,
            'style': style,
            'div': div,
            'a': a,
            'class': classes,
            'font': font,
            'span': span,
            'object': object,
            'iframe': iframe,
            'script': script
        },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        sucess: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info");
        }
    });


}
//绑定数据
function ShowCollectSite(SiteId) {
    $.ajax({
        url: '/AjaxCollectSite/GetCollectSiteById.cspx?siteId=' + SiteId,
        type: 'POST',
        datatype: 'json',
        success: function (json) {
            $('#SiteName').val(json.SiteName);
            $('#drpSiteFolderId').val(json.SiteFolderId + "&" + json.ChannelId);
            $('#SiteURL').val(json.SiteURL);
            $('#Encode').val(json.Encode);
             $('#drpAudit').val(json.Audit);
             //采集参数
             $('#chSavePic').val(json.SavePic);
             $('#chReverse').val(json.Reverse);
             $('#chPicNews').val(json.PicNews);
             //过滤选项
             $('#ChbHTML').val(json.HTML);
             $('#ChbSTYLE').val(json.STYLE);
             $('#ChbDIV').val(json.DIV);
             $('#ChbA').val(json.A);
             $('#ChbCLASS').val(json.CLASS);
             $('#ChbFONT').val(json.FONT);
             $('#ChbSPAN').val(json.SPAN);
             $('#ChbOBJECT').val(json.OBJECT);
             $('#ChbIFRAME').val(json.IFARME);
             $('#ChbSCRIPT').val(json.SCRIPT);
         }
    });
 }
 //编辑数据
 function EditCollectSite(SiteId) {
     if (ValidateForm() == false) return;
     var siteName = $('#SiteName').val();
     var siteFolderIds = $('#drpSiteFolderId option:selected').val();
     var siteFolderId = siteFolderIds.split('&')[0];
     var channelId = siteFolderIds.split('&')[1];
     var siteURL = $('#SiteURL').val();
     var encode = $('#Encode').val();
     var audit = $('#drpAudit option:selected').val();
     //采集参数
     var savePic = $('#chSavePic').val();
     var reverse = $('#chReverse').val();
     var picNews = $('#chPicNews').val();
     //过滤选项
     var html = $('#ChbHTML').val();
     var style = $('#ChbSTYLE').val();
     var div = $('#ChbDIV').val();
     var a = $('#ChbA').val();
     var classes = $('#ChbCLASS').val();
     var font = $('#ChbFONT').val();
     var span = $('#ChbSPAN').val();
     var object = $('#ChbOBJECT').val();
     var iframe = $('#ChbIFRAME').val();
     var script = $('#ChbSCRIPT').val();
     var j_waitDialog = ShowWaitMessageDialog();
     $.ajax({
         url: '/AjaxCollectSite/UpdateCollectSite.cspx',
         type: 'POST',
         data: { 'siteId': SiteId,
             'siteName': siteName,
             'siteFolderId': siteFolderId,
             'siteURL': siteURL,
             'encode': encode,
             'channelId': channelId,
             'audit': audit,
             'savePic': savePic,
             'reverse': reverse,
             'picNews': picNews,
             'html': html,
             'style': style,
             'div': div,
             'a': a,
             'class': classes,
             'font': font,
             'span': span,
             'object': object,
             'iframe': iframe,
             'script': script
         },
         complete: function () { HideWaitMessageDialog(j_waitDialog); },
         sucess: function (responseText) {
             $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                 window.open('collect_sitetwo.aspx?siteId' + SiteId, target = 'main');
             });
         }
     });
 }
 function ChangeUrl() {
     var obj = document.getElementById("A_Preview");
     obj.href = $("#SiteURL").val();
 }
 function ChooseEncode(obj) {
     $("#Encode").text('');
     $("#Encode").text(obj.innerText);
 }
//验证方法
function ValidateForm() {
    if (ValidateControl("#SiteName", "采集站点名称 不能为空。") == false) return false;
    if (ValidateControl("#drpSiteFolderId", "采集站点分类 不能为空。") == false) return false;
    if (ValidateControl("#SiteURL", "采集对象页 不能为空。") == false) return false;
    if (ValidateControl("#Encode", "采集页编码方式 不能为空。") == false) return false;
    return true;
}