$(function () {
    //绑定数据
    GetBindData();
    //下一步
    $("#btnNext").click(function () {
        GetNext();
    });
});
//绑定数据
function GetBindData() {
    var siteId = $('#HidSiteID').val();
    if (siteId == '') {
        alert('没有传送必要的参数');
    } else {
    $.ajax({
        url: '/AjaxCollectSite/GetCollectSiteTwoById.cspx?siteId=' + siteId,
        type: 'POST',
        datatype: 'json',
        success: function (json) {
            $("#txtListSetting").val(json.ListSetting);
            var nPageType = 0;
            if (json.OtherType != '' || json.OtherType != null) {
                nPageType = json.OtherType;
            }
            if (nPageType == 1) {
                $("#RadPageFlag").attr("checked", "checked");
                $('#EdtPageFlag').val(json.OtherPageSetting);
            } else if (nPageType == 2) {
                $("#RadPageSingle").attr("checked", "checked");
                $('#EdtPageFlag').val(json.OtherPageSetting);
            } else if (nPageType == 3) {
                $("#RadPageIndex").attr("checked", "checked");
                $('#EdtPageIndex').val(json.OtherPageSetting);
                $('#TxtPageStart').val(json.StartPageNum);
                $('#TxtPageEnd').val(json.EndPageNum);

            } else {
               $("#RadPageNone").attr("checked", "checked");
            }
        }
    });
    }

 }
//下一步
function GetNext() {
    var siteId = $('#HidSiteID').val();
    var listSetting = $('#txtListSetting').val();
    var otherPageSetting = '';
    var otherType = '';
    var startPageNum = $('#TxtPageStart').val();
    var endPageNum = $('#TxtPageEnd').val();
    if (listSetting.indexOf('[列表内容]') < 0) {
        alert('请指定列表内容');
        return;
    } else {
        var hdurlSiteID = $('#hdurlSiteID').val();
        //分页设定
        if ($("#RadPageNone").attr("checked") == "checked") {
            $("#RadPageFlag").attr("checked", "");
            $("#RadPageSingle").attr("checked", "");
            $("#RadPageIndex").attr("checked", "");
            otherType = 0;
        } else if ($("#RadPageFlag").attr("checked") == "checked") {
            $("#RadPageNone").attr("checked", "");
            $("#RadPageSingle").attr("checked", "");
            $("#RadPageIndex").attr("checked", "");
            if ($("#EdtPageFlag").val().indexOf('[其他页面]') < 0) {
                alert("您已经设置分页方式为递归分页,请设置分页规则(必须包含\"[其他页面]\"标识)!");
                return;
             }
            otherType = 1;
            otherPageSetting = ('EdtPageFlag').val();
        } else if ($("#RadPageSingle").attr("checked") == "checked") {
            $("#RadPageNone").attr("checked", "");
            $("#RadPageFlag").attr("checked", "");
            $("#RadPageIndex").attr("checked", "");
            if ($("#EdtPageFlag").indexOf('[其他页面]') < 0) {
                alert("您已经设置分页方式为单页分页,请设置分页规则(必须包含\"[其他页面]\"标识)!");
                return;
            }
            otherType = 2;
            otherPageSetting = ('EdtPageFlag').val();
        } if ($("#RadPageIndex").attr("checked") == "checked") {
            $("#RadPageNone").attr("checked", "");
            $("#RadPageFlag").attr("checked", "");
            $("#RadPageSingle").attr("checked", "");
            otherType = 3;
            if ($('#EdtPageIndex').val().indexOf('[页码]')<0) {
                alert("您已经设置分页方式为索引分页,请设置索引规则(必须包含\"[页码]\"标识)!");
                return;
            }
            if ($('#TxtPageStart').val() == "" || $('#TxtPageEnd').val() == "") {
                alert("您已经设置分页方式为索引分页,请设置开始页码和结束页码!");
                return;
            }
        }
        $.ajax({
            url: '/AjaxCollectSite/UpdateCollectSiteTwo.cspx',
            type: 'POST',
            data: { 'siteId': siteId,
                'listSetting': listSetting,
                'otherPageSetting': otherPageSetting,
                'otherType': otherType,
                'startPageNum': startPageNum,
                'endPageNum': endPageNum
            },
            datatype: 'json',
            success: function () {
                window.open('collect_sitethree.aspx?siteId=' + siteId, target = 'main');
            }
        });

    }
 }
