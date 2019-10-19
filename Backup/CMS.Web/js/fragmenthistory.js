$(function () {
    //恢复历史记录
    $("#tableHistory a").each(function () {
        this.onclick = function () {
            if (confirm('确定要恢复数据吗?')) {
                var j_waitDialog = ShowWaitMessageDialog();
                var thisObj = this.parentNode.parentNode;
                var a = $(thisObj).children();
                var aids = $(this).attr("id");
                var aid = aids.split('&')[1];
                var fragmentId = $("#hdFragmentId" + aid + "").val();
                var content = $("#hdContent" + aid + "").val();
                var channelId = $("#hdChannelID" + aid + "").val();
                var orderName = $("#hdOrderNum" + aid + "").val();
                var typeId = $("#hdTypeId" + aid + "").val();
                var IsDelete = $("#hdIsDelete" + aid + "").val();
                var person = window.dialogArguments;
                if (content != undefined) {
                    $.ajax({
                        url: '/AjaxFragment/UpdateFragmentBack.cspx',
                        data: { 'fragmentId': fragmentId,
                            'content': content,
                            'channelId': channelId,
                            'orderName': orderName,
                            'typeId': typeId,
                            'IsDelete': IsDelete
                        },
                        type: 'POST',
                        datatype: 'json',
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function () {
                            window.returnValue = content;
                            window.close();
                        }
                    });
                }
            }
        }
    });
});