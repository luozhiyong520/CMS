$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //分页列表
    $('#tblQueryResult').html('');
    BataSearchMedia();
    //添加文件
    $("#MediaInfo").click(function () {
        $("#UploadPath").val("");
        $("#MediaLabel").val("");
        $("#MediaDescript").val("");
        ShowEditItemDialog('', 'MediaAddEdit', 800, 560, UploadMedia);
       // showUpload();
        return false;
    });


    //查询
    $("#btnQuery").click(function () {
        $('#tblQueryResult').html('');
        BataSearchMedia();
    });

    //删除
    $("#Submit1").click(function () {
        if (confirm('你确定删除该图片吗？')) {
            var mstr = $(".datagrid :input").fieldSerialize();
            if (mstr == "") {
                alert("你还没有选择要删除的文件!");
                return;
            }
            $.ajax({
                type: "GET",
                url: "/AjaxMedia/DelAllMedia.cspx",
                data: mstr,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("删除成功");
                        window.location = window.location;
                    }
                }
            });
        }
    });
});


function showUpload() {
    var swfu;
   
   // window.onload = function () {
        alert('fasdf')
        var settings = {
            flash_url: "/themes/default/scripts/swfupload/swfupload.swf",
            upload_url: "upload.ashx",
            file_size_limit: "100 MB",
            file_types: "*.*",
            file_types_description: "All Files",
            file_upload_limit: 100,
            file_queue_limit: 0,
            custom_settings: {

                progressTarget: "divprogresscontainer",
                progressGroupTarget: "divprogressGroup",

                //progress object
                container_css: "progressobj",
                icoNormal_css: "IcoNormal",
                icoWaiting_css: "IcoWaiting",
                icoUpload_css: "IcoUpload",
                fname_css: "fle ftt",
                state_div_css: "statebarSmallDiv",
                state_bar_css: "statebar",
                percent_css: "ftt",
                href_delete_css: "ftt",

                //sum object
                /*
                页面中不应出现以"cnt_"开头声明的元素
                */
                s_cnt_progress: "cnt_progress",
                s_cnt_span_text: "fle",
                s_cnt_progress_statebar: "cnt_progress_statebar",
                s_cnt_progress_percent: "cnt_progress_percent",
                s_cnt_progress_uploaded: "cnt_progress_uploaded",
                s_cnt_progress_size: "cnt_progress_size"
            },
            debug: false,

            // Button settings
            button_image_url: "images/TestImageNoText_65x29.png",
            button_width: "65",
            button_height: "29",
            button_placeholder_id: "spanButtonPlaceHolder",
            button_text: '<span class="theFont">上传文件</span>',
            button_text_style: ".theFont { font-size: 12;color:#0068B7; }",
            button_text_left_padding: 12,
            button_text_top_padding: 3,

            // The event handler functions are defined in handlers.js
            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,
            file_dialog_complete_handler: fileDialogComplete
        };
      
        swfu = new SWFUpload(settings);
        // };
        alert('bb')
   
}

function UploadMedia(j_dialog) {
   alert('fasdf')
    var UploadPath = $("#preview").attr("src");
    var submitUrl = "/AjaxMedia/UploadPic.cspx?UploadPath=" + UploadPath + "&UploadType=add";
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
        error:function(XMLHttpRequest, textStatus, errorThrown){
             alert("XMLHttpRequest.status="+XMLHttpRequest.status+"\nXMLHttpRequest.readyState="+XMLHttpRequest.readyState+"\ntextStatus="+textStatus);
              }
        });

};
 




function editMedia() {
    var dom = this;
    var MediaId = $(this).attr("rowId");
    $.ajax({
        url: "/AjaxMedia/GetMediaInfo.cspx?mediaid=" + MediaId,
        dataType: "json",
        success: function (json) {
            $("#MediaClassId option[value=" + json.MediaClassId + "]").attr("selected", "selected");
            $("#MediaLabel").val(json.MediaLabel);
            $("#MediaDescript").val(json.MediaDescript);
            $("#MediaTitle").val(json.MediaTitle);

            // 显示编辑对话框
            ShowEditItemDialog("edit", 'MediaAddEdit', 800, 560, function (j_dialog) {
                var submitUrl = "/AjaxMedia/UploadPic.cspx?UpFilePath=UploadPath&UploadType=edit&MediaId=" + MediaId;
                //开始提交
                $("#form1").ajaxSubmit({
                    url: submitUrl,
                    type: "POST",
                    success: function (responseText) {
                        $.messager.alert(g_MsgBoxTitle, responseText, "info", function () {
                            j_dialog.hide().dialog('close');
                            $('#tblQueryResult').datagrid("reload");
                        });
                    }
                });
            });


        }
    })

    return false;
}


//绑定模糊查询数据
function BataSearchMedia() {
    var dateRange = GetDateRange2("txtStartDate", "txtEndDate");
    var StartDate =  dateRange.StartDate
    var EndDate= dateRange.EndDate
    var txtKeyword = $("#txtKeyword").val();
    var MediaClass = $("#MediaClass").val();
    $('#tblQueryResult').datagrid({
        title: "文件列表",
        height: getHeight(),
        fitColumns : true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'MediaId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        type: "POST",
        url: '/AjaxMedia/SearchMedia.cspx?txtKeyword=' + encodeURIComponent(txtKeyword) + '&txtStartDate=' + StartDate + '&txtEndDate=' + EndDate + '&MediaClass=' + MediaClass,
        columns: [[
             { title: "<b>选择</b>", field: "MediaId", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     return value = '<input name="MediaId" id="MediaId" type="checkbox" value="' + row.MediaId + '" onclick="tes();" />';
                 }
             },


			 { title: "<b>缩略图</b>", field: "UploadPath", align: 'center', width: fillSize(0.2),
			     formatter: function (value, row) {
			         return value = '<IMG height=100 src="http://img.stock.com' + row.UploadPath + '" width=120 align=absMiddle>';
			     }
			 },

			 { title: "<b>标题</b>", field: "MediaTitle", align: 'center', width: fillSize(0.2) },
             { title: "<b>文件大小</b>", field: "MediaSize", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     return value = row.MediaSize+"KB";
                 }
              },
             { title: "<b>上传时间</b>", field: "UploadTime", align: 'center', width: fillSize(0.15),
                 formatter: function (value, row) {
                     var UploadDate = row.UploadTime.replace("/Date(", "").replace(")/", "");

                     return value = getLocalTime(UploadDate.substr(0, 10));
                 }
             },
             { title: "<b>文件类别</b>", field: "MediaClassName", align: 'center', width: fillSize(0.05) },
             { title: "<b>上传人</b>", field: "Uploader", align: 'center', width: fillSize(0.1) },
             { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.2),
                 formatter: function (value, row) {
                     return value = '<a  href="javascript:void(0);" class="easyui-linkbutton" rowId="' + row.MediaId + '" plain="true">' +
                           '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>' +
                           '&nbsp;&nbsp;<a href="javascript:void(0);" returnUrl="/AjaxMedia/DelMedia.cspx?MediaId=' + row.MediaId + '" title="删除" class="easyui-linkbutton" plain="true">' +
                           '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			.filter(g_deleteButtonFilter).click(DelMediaById).end()
            .filter("a[rowId]").click(editMedia);
        }
    });
}

//删除图片
function DelMediaById() {
    var grid = $('#tblQueryResult');
    if (confirm('确定要删除此记录吗？')) {
        $.ajax({
            url: $(this).attr("returnUrl"),
            success: function (responseText) {
                if (responseText =="000000") {
                    $.messager.alert(g_MsgBoxTitle, "删除记录成功!", "info", function () {
                        grid.datagrid('reload');
                    });

                }
                else {
                    $.messager.alert(g_MsgBoxTitle, "记录删除失败!", "info", function () {
                        grid.datagrid('reload');
                    });
                }
             }
        });
    }
 }

 

//是否显示水印
function showAndHiddenSet(valueNum) {
    var state = valueNum == 1 ? "" : "none";
    var data = $('.water');
    for (i = 0; i < data.length; i++) {
        data[i].style.display = state;
    }
    if (valueNum == 1) {
        showImage();
    }
}

function showIMG() {
    document.getElementById("pic").innerHTML = "<img width='106' height='50'  src='" + $('imgId').value + "'>";
}
