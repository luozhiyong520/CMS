$(function () {
    //初始化数据
    $.ajax({
        type: "POST",
        url: "/AjaxSendMsg/GetMsgTypeData.cspx",
        success: function (data) {
            var st = eval(data);
            var soption = " <option value=\"0\">--请选择--</option>";
            var shtml = "";
            for (var i = 0; i < st.length; i++) {
                shtml += "<option value='" + st[i].MsgTypeId + "'>" + st[i].TypeContent + "</option>";
            }
            $("#typeContent").html(soption + shtml);
            BataSearchPageSendMsg();
        }
    });
    self.parent.document.body.style.overflow = 'hidden';
    $('.datagrid-btable').html('');
    $("#btnSearch").click(function () {
        $('.datagrid-btable').html('');
        BataSearchPageSendMsg();
    })
    $("table a.linkbutton[rowId]").click(windsopen);
});

//绑定查询数据
function BataSearchPageSendMsg() {
    //    var TypeContent = '', MsgType = '', tuTimeStart = '', tuTimeEnd = '', ReplyTimeStart = '', ReplyTimeEnd = '', YesOrNo = '', Editor = '';
    //    TypeContent = $("#typeContent").val();
    //    MsgType = $("#MsgType").val();
    //    tuTimeStart = $("#tuTimeStart").val();
    //    tuTimeEnd = $("#tuTimeEnd").val();
    //    ReplyTimeStart = $("#ReplyTimeStart").val();
    //    ReplyTimeEnd = $("#ReplyTimeEnd").val();
    //    YesOrNo = $("#YesOrNo").val();
    //    Editor = $("#Editor").val();
    var params = $(".cz_xx :input").fieldSerialize();
    // var param = "TypeContent=" + TypeContent + "&MsgType=" + MsgType + "&TuTimeStart=" + tuTimeStart + "&TuTimeEnd=" + tuTimeEnd + "&ReplyTimeStart=" + ReplyTimeStart + "&ReplyTimeEnd=" + ReplyTimeEnd + "&YesOrNo=" + YesOrNo + "&Editor=" + encodeURI(Editor);
    $('#SendMsgInfo').datagrid({
        title: "用户留言列表",
        height: self.parent.document.body.clientHeight - 150,
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'MsgId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        url: '/AjaxSendMsg/SearchSendMsg.cspx?' + params,
        columns: [[
            { hidden: true, field: "MsgId" },
             { title: "<b>问题类型</b>", field: "TypeContent", width: fillSize(0.08)
             },
			 { title: "<b>问题内容</b>", field: "MsgContent", width: fillSize(0.27),
			     formatter: function (value, row) {
			         var bitImgUrl = (row.ImgUrl == '' || row.ImgUrl == null) ? "" : "<b>有图片</b>";
			         return "<a href='javascript:windsopen();' rowtid='" + row.MsgId + "'class='easyui-linkbutton' plain='true'>" + bitImgUrl + "&nbsp;&nbsp;&nbsp;&nbsp;" + value + "</a>";
			     }
			 },
              { title: "<b>提问人</b>", field: "QuCustomerName", width: fillSize(0.1) },
              { title: "<b>联系电话</b>", field: "Tel", width: fillSize(0.1),
                  formatter: function (value, row) {
                      000000
                      // alert(row.Tel.substring(0,row.Tel.length - 4))
                      var Tel = "";
                      if (row.MsgTypeId == 8) {
                          if (row.PhoneOrEmail != null) {
                              if (row.PhoneOrEmail.indexOf('@') > 0)
                                  Tel = row.PhoneOrEmail
                              else
                                  Tel = (row.PhoneOrEmail == '' || row.PhoneOrEmail == null) ? '' : (row.PhoneOrEmail.substring(0, row.PhoneOrEmail.length - 4) + '****');
                          }
                      } else {
                          Tel = (row.Tel == '' || row.Tel == null) ? '' : (row.Tel.substring(0, row.Tel.length - 4) + '****');
                      }
                      return Tel;
                  }
              },
			 { title: "<b>提问时间</b>", field: "QuTime", align: 'center', width: fillSize(0.1),
			     formatter: function (value) {
			         QuTime = ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss');
			         return QuTime;
			     }
			 },
             { title: "<b>回复内容</b>", field: "ReplyContent", align: 'center', width: fillSize(0.2),
                 formatter: function (value) {
                     return ReplyContent = value;
                 }
             },
             { title: "<b>操作人</b>", field: "Editor", align: 'center', width: fillSize(0.1),
                 formatter: function (value) {
                     return Replier = value;
                 }
             },
             { title: "<b>回复时间</b>", field: "ReplyTime", align: 'center', width: fillSize(0.1),
                 formatter: function (value) {
                     if (value != null) {
                         return ReplyTime = ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss');
                     }
                     else if ((ReplyContent == "" || ReplyContent == null) && (Replier == "" || Replier == null)) {
                         return ReplyTime = "";
                     }
                     return ReplyTime;
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#SendMsgInfo').datagrid("getPanel")).find('a.easyui-linkbutton')
                 .filter("a[rowtid]").click(windsopen).end();
        }
    });
}
//打开新页面的方法
function windsopen() {
    var dom = this;
    var grid = $('#SendMsgInfo');
    var msgid = $(this).attr("rowtid");
    var WWidth = (window.screen.width - 500) / 2;
    var Wheight = (window.screen.height - 150) / 2;
    if (msgid != undefined) {
        var rows = $("#SendMsgInfo").datagrid("getRows");
        var value = '';
        for (var i = 0; i < rows.length; i++) {
            value += value = '' ? '' : ',' + rows[i].MsgId;
        }
        value = value.substring(1, value.length);
        var returnvalue = window.showModalDialog('sendmsgedit.aspx?msgid=' + msgid + '&paramslist=' + value, 'dialogHeight=750px,dialogWidth=675px');
        if (returnvalue > 0) {
            grid.datagrid('reload');
        }
    }
}
