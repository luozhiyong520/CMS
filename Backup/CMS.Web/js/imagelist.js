

$(function () {
   
    //分页列表
    $('#tblQueryResult').html('');
    BataSearchMedia();

    //查询
    $("#btnQuery").click(function () {
        $('#tblQueryResult').html('');
        BataSearchMedia();
    });

    //添加文件
    $("#MediaInfo").click(function () {

        $("#UploadPath").val("");
        $("#MediaLabel").val("");
        $("#MediaDescript").val("");
        $("#MediaAddEdit").css("display", "block");
        return false;
    });

    $("#closepic").click(function () {
        $("#MediaAddEdit").css("display", "none");
        $('#tblQueryResult').datagrid("reload");
    });

    //批量插入
    $("#bulkInsert").click(function () {
        var pstr = $(".datagrid :input").fieldSerialize();
        $.ajax({
            type: "GET",
            url: "/AjaxMedia/GetMediaImage.cspx",
            data: pstr,
            success: function (responseText) {
                window.returnValue = responseText;
                window.close();
            }
        });
    });

})

function getLocalTime(nS) {
    return new Date(parseInt(nS) * 1000).toLocaleString().replace(/年|月/g, "-").replace(/日/g, " ");
}


//单个文件上传
function UploadMedia(j_dialog) {
    var UploadPath = $("#preview").attr("src");
    // var submitUrl = "/AjaxMedia/UploadPic.cspx?UploadPath=" + UploadPath + "&UploadType=add";
    var submitUrl = "/AjaxMedia/UploadPic.cspx?UpFilePath=UploadPath&UploadType=add";
    //开始提交
    $("#form1").ajaxSubmit({
        url: submitUrl,
        type: "GET",
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, responseText, "info", function () {
                j_dialog.hide().dialog('close');
                $('#tblQueryResult').datagrid("reload");
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("XMLHttpRequest.status=" + XMLHttpRequest.status + "\nXMLHttpRequest.readyState=" + XMLHttpRequest.readyState + "\ntextStatus=" + textStatus);
        }
    });

};



//绑定模糊查询数据
function BataSearchMedia() {
 
    var dateRange = GetDateRange2("txtStartDate", "txtEndDate");
    var StartDate = dateRange.StartDate
    var EndDate = dateRange.EndDate
    var txtKeyword = $("#txtKeyword").val();
    var MediaClass = $("#MediaClass").val();
    $('#tblQueryResult').datagrid({
        title: "文件列表",
        height: 480,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'MediaId',
        pageSize: 5, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [2, 5, 10, 15], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: false, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        type: "POST",
        url: '/AjaxMedia/SearchMedia.cspx?txtKeyword=' + encodeURIComponent(txtKeyword) + '&txtStartDate=' + StartDate + '&txtEndDate=' + EndDate + '&MediaClass=' + MediaClass,
        columns: [[
             { title: "选择", field: "MediaId", width: 60,
                 formatter: function (value, row) {
                     return value = '<input name="MediaId" id="MediaId" type="checkbox" value="' + row.MediaId + '" onclick="tes();"/>';
                 }
             },

			 { title: "缩略图", field: "Content", width: 200,
			     formatter: function (value, row) {
			         return value = '<IMG height=100 src="' + row.UploadPath + '" width=120 align=absMiddle>';
			     }
			 },

			 { title: "标题", field: "MediaTitle", width: 100 },
             { title: "文件大小", field: "MediaSize", width: 80 },
             { title: "上传时间", field: "UploadTime", width: 130,
                 formatter: function (value, row) {
                     var UploadDate = row.UploadTime.replace("/Date(", "").replace(")/", "");
                     return value = getLocalTime(UploadDate.substr(0, 10));
                 }
             },
             { title: "上传人", field: "Uploader", width: 60 },
             { title: "操作", field: "xx", width: 100,
                 formatter: function (value, row) {
                     return value = '<a  href="javascript:void(0);" class="easyui-linkbutton" rowId="' + row.MediaId + '" plain="true">插入图片</a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			.filter(g_deleteButtonFilter).click(CommonDeleteRecord).end()
            .filter("a[rowId]").click(selectedimage);
        }
    });


}

//插入单张图片
function selectedimage() {
    var MediaId = $(this).attr("rowId");
    $.ajax({
        type: "POST",
        url: "/AjaxMedia/GetMediaInfo.cspx?mediaid=" + MediaId,
        dataType: "json",
        success: function (json) {
            window.returnValue = json.UploadPath;
            window.close();
        }
    });
}

function setChecked() {
    var ids = $("input[type='checkbox']");
    var n;
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].checked == true) {
            ids[i].checked = "";
            n = false;
        } else {
            ids[i].checked = "checked";
            n = true;
        }
    }

    if (n == false) {
        $("#bulkInsert").attr('disabled', 'disabled');
    }
    else {

        $("#bulkInsert").removeAttr('disabled');
    }
}


function tes() {
    var sid = $("input[type='checkbox']");
    for (var i = 0; i < sid.length; i++) {
        if (sid[i].checked == true) {
            $("#bulkInsert").removeAttr('disabled');
            return;
        } else {
            $("#bulkInsert").attr('disabled', 'disabled');
        }
    }
}
 