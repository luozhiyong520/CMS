$(function () {
    //  self.parent.document.body.style.overflow = 'hidden';
    //分页列表
    $('#tblQueryResult').html('');
    BataSearchMedia();
    //添加文件
    $("#MediaInfo").click(function () {
        $("#UploadPath").val("");
        $("#MediaLabel").val("");
        $("#MediaDescript").val("");
        //ShowEditUploadDialog('', 'MediaAddEdit', 800, 560);
        $("#MediaAddEdit").css("display", "block");
        return false;
    });

    $("#closepic").click(function () {
        $("#MediaAddEdit").css("display", "none");
        $('#tblQueryResult').datagrid("reload");
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
			         return value = '<IMG height=100 src="' + row.UploadPath + '" width=120 align=absMiddle>';
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
                     //return value = '<a  href="javascript:void(0);" class="easyui-linkbutton" rowId="' + row.MediaId + '" plain="true">' +
                     //      '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>' +
                     //      '&nbsp;&nbsp;<a href="javascript:void(0);" returnUrl="/AjaxMedia/DelMedia.cspx?MediaId=' + row.MediaId + '" title="删除" class="easyui-linkbutton" plain="true">' +
                     //      '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                     return value = '<a  href="javascript:void(0);"  class="easyui-linkbutton" rowId="http://img.upchina.com' + row.UploadPath + '" plain="true">' +
                           '复制图片地址</a>' +
                         '&nbsp;&nbsp;<a href="javascript:void(0);" returnUrl="/AjaxMedia/DelMedia.cspx?MediaId=' + row.MediaId + '" title="删除" class="easyui-linkbutton" plain="true">' +
                           '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			.filter(g_deleteButtonFilter).click(DelMediaById).end()
            .filter("a[rowId]").click(copyToClipboard);
        }
    });
}


function copyToClipboard() {
    var dom = this;
    var txt = $(this).attr("rowId");
             if (window.clipboardData) {
                 window.clipboardData.clearData();
                 clipboardData.setData("Text", txt);
                 alert("复制成功！");
 
             } else if (navigator.userAgent.indexOf("Opera") == -1) {
                window.location = txt;
             } else if (window.netscape) {
                 try {
                     netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                 } catch (e) {
                     alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将 'signed.applets.codebase_principal_support'设置为'true'");
                 }
                 var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
                 if (!clip)
                     return;
                 var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
                 if (!trans)
                     return;
                 trans.addDataFlavor("text/unicode");
                 var str = new Object();
                 var len = new Object();
                 var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
                 var copytext = txt;
                 str.data = copytext;
                 trans.setTransferData("text/unicode", str, copytext.length * 2);
                 var clipid = Components.interfaces.nsIClipboard;
                 if (!clip)
                     return false;
                 clip.setData(trans, null, clipid.kGlobalClipboard);
                 alert("复制成功！");
             }
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
