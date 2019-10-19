$(function () {
    var mydate = new Date();
    var year = mydate.getFullYear();
    var month = mydate.getMonth() + 1;
    var day = mydate.getDate();
    GetData("", year + "-" + month + "-" + day + " 23:59:59", 0, 0);

    $("#btnQuery").click(function () {
        var dateRange = GetDateRange2("StartTime", "EndTiem");
        var txtStart = dateRange.StartDate == "" ? "" : dateRange.StartDate + " 00:00:00";
        var txtEnd = dateRange.EndDate == "" ? "" : dateRange.EndDate + " 23:59:59";
        var channelId = $("#ChannelId").val();
        var adminName = $("#AdminId").val();
        if (txtEnd == "") {
            txtEnd = year + "-" + month + "-" + day + " 23:59:59";
        }
        GetData(txtStart, txtEnd, adminName, channelId);
    })
})


function GetData(txtStart, txtEnd, adminName, channelId) {
    $.ajax({
        url: '/AjaxInformationStatistic/GetInformationStatistics.cspx',
        type: 'POST',
        data: { 'stratTime': txtStart, 'endTime': txtEnd, 'adminName': adminName, 'channelId': channelId },
        success: function (data) {
            if (data != "") {
                var Hits;
                var Con;
                var sum = 0;   //统计点击总数
                var stict = 0; //统计编辑总数
                var trader = "<table cellpadding=\"0\" cellspacing=\"0\" class=\"c_table\" id=\"thetable\">" +
                    "<TR>" +
                        "<TH width=\"10%\" nowrap>用户名</TH>" +
                        "<TH width=\"10%\" nowrap>栏目名称</TH>" +
                        "<TH width=\"20%\" nowrap>开始时间</TH>" +
                        "<TH width=\"20%\" nowrap>结束时间</TH>" +
                        "<TH width=\"20%\" nowrap>编辑次数</TH>" +
                        "<TH width=\"20%\" nowrap>点击次数</TH>" +
                    "</TR>"
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Num == 0) {
                        Hits = 0;
                    } else {
                        Hits = data[i].Num;
                        sum += Hits;
                    }

                    if (data[i].Con == 0) {
                        Con = 0;
                    } else {
                        Con = data[i].Con;
                        stict += Con;
                    }

                    trader += "<tr class=\"td_ys\"><td width=\"10%\" align=center>" + data[i].CreatedUser + "</td>" +
                    "<td width=\"10%\" align=\"center\">" + data[i].ChannelName + "</td>" +
                    "<td width=\"10%\" align=\"center\">" + txtStart + "</td>" +
                    "<td width=\"20%\" align=\"center\">" + txtEnd + "</td>" +
                    "<td width=\"20%\" align=\"center\">" + data[i].Con + "</td>" +
                    "<td width=\"20%\" align=\"center\">" + Hits + "</td></tr>"
                }
                trader += "<tr><td width=\"10%\" align=\"center\">总次数</td><td width=\"10%\" align=\"center\"></td><td width=\"10%\" align=\"center\"></td><td width=\"10%\" align=\"center\"></td><td width=\"10%\" align=\"center\">" + stict + "</td><td width=\"10%\" align=\"center\">" + sum + "</td></tr></table>";
                $("#tblQueryResult").html(trader);
            } else {
                $("#tblQueryResult").html('');
                alert("暂时没有数据");
            }
        }
    });
}

 // 根据二个字符串，返回一个日期范围。
         function GetDateRange2(txtStart, txtEnd) {
             var _date1 = $("#" + txtStart).datebox("getValue");
             var _date2 = $("#" + txtEnd).datebox("getValue");

             var _d1 = parseDate(_date1);
             var _d2 = parseDate(_date2);
             if (_date1.length > 0 && _d1 == null) {
                 alert("日期格式输入无效。"); $("#" + txtStart).focus(); return null;
             }
             if (_date2.length > 0 && _d2 == null) {
                 alert("日期格式输入无效。"); $("#" + txtEnd).focus(); return null;
             }
             if (_date1.length > 0 && _date2.length > 0 && _d1 > _d2) {
                 alert("日期范围输入无效。"); $("#" + txtEnd).focus(); return null;
             }
             var obj = { StartDate: _date1, EndDate: _date2 };
             return obj;
         }