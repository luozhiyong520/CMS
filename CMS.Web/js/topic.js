$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //初始化数据
    $('.datagrid-btable').html('');
    $.ajax({
        url: "/AjaxTopic/GetTopicType.cspx",
        type: "POST",
        success: function (responseText) {
            var op = " <option value=\"\">--请选择--</option>";
            $("#drptypeTemplateId").append(op + responseText);
        }
    });
    $.ajax({
        url: "/AjaxTopic/GetDrpTopicType.cspx",
        type: "POST",
        success: function (responseText) {
            var op = " <option value=\"0\">--请选择--</option>";
            $("#drpTopicTypeName").append(op + responseText);
            $("#drpserachTopicTypeName").append(op + responseText);
            BataSearchTopic();
        }
    });
    //添加事件
    $("#addtopictype").click(function () {
        $("#txttypeTopicTypeName").val('');
        $("#drpTopicTypeGenerate option:selected").val('');
        $("#drptypeTemplateId option:selected").val('');
        ShowEditItemDialog('', 'divbodytype', 510, 300, InsertTopicType);

        return false;
    });
    $("#closepic").click(function () {
        $("#divbody").css("display", "none");
        //$('#tblQueryResult').datagrid("reload");
    });
    $("#addtopic").click(function () {
        $("#imagesrc>span").remove();
        $("#imagesrc>img").remove();
        $("#imagesrc>input").remove();
        $("#txtTopicName").val('');
        $("#documentsrc>span").remove();
        $("#documentsrc>input").remove();
        // $("#drpTopicTypeName").val('');
        $("#txtRemark").val('');
        $("#txtPublishTime").val('');
        $("#imagesrc").html('');
        $("#CreatedTime").val(new Date().format("yyyy-MM-dd hh:mm:ss"));
        //ShowEditItemDialog('', 'divbody', 720, 400, InsertTopic);
        $("#btn_save").css("display", "");
        $("#btn_editsave").css("display", "none");
        $("#divbody").css("display", "");
        return false;
    });

    //查询
    $("#btnSearch").click(function () {
        //$("#pgtopic").html('');
        $('.datagrid-btable').html('');
        BataSearchTopic();
    });
    //提交
    $("#btn_save").click(function () {
        InsertTopic();
    });
    //提交
    $("#btn_editsave").click(function () {
        editsave();
    });
    //取消
    $("#btn_cancel").click(function () {
        $("#divbody").css("display", "none");
    });
    $("table a.linkbutton[rowId]").click(BataSearchTopic);
});

//绑定模糊查询数据
function BataSearchTopic() {
    var topicName = $('#txtserachTopicName').val();
    var topicTypeId = $('#drpserachTopicTypeName option:selected').val();

    $('#pgtopic').datagrid({
        title: "专题列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'TopicId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选 
        showPageList: false,
        fitColumns: true,
        url: 'AjaxTopic/SearchTopic.cspx?topicName=' + encodeURIComponent(topicName) + '&topicTypeId=' + topicTypeId,
        columns: [[
             
             { title: "<b style=\"padding-left: 40%;\">专题名称</b>", field: "TopicName", width: fillSize(0.2) },
			 { title: "<b>专题类型</b>", field: "TopicTypeName", align: 'center', width: fillSize(0.1) },
             { title: "<b style=\"padding-left: 45%;\">备注</b>", field: "Remark", width: fillSize(0.3) },
             { title: "<b>状态</b>", field: "Status", align: 'center', width: fillSize(0.07) },
			 { title: "<b>创建者</b>", field: "CreatedUser", align: 'center', width: fillSize(0.08) },
             { title: "<b>发布时间</b>", field: "CreatedTime", align: 'center', width: fillSize(0.1),
                 formatter: function (value) {
                     return ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss')
                 }
             },
             { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.15),
                 formatter: function (value, row, index) {
                    // if (row.TopicTypeId == 3 || row.TopicTypeId == 1 || index < 20 || row.TopicTypeId == 5 || row.TopicTypeId == 6)
                        return value = '<a target="_blank" href="../preview.aspx?TemplateId=' + row.TemplateId + '&ReDate=' + ConertJsonTimeAndFormat(row.CreatedTime, 'yyyy-MM-dd') + '"  class="easyui-linkbutton"  plain="true">预览</a>&nbsp;&nbsp;<a href="javascript:void();"  class="easyui-linkbutton" onclick="CreatePage(' + row.TopicId + ',\'' + ConertJsonTimeAndFormat(row.CreatedTime, 'yyyy-MM-dd') + '\')" plain="true">发布</a>&nbsp;&nbsp;<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.TopicId + '" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxTopic/DeleteTopic.cspx?topicId=' + row.TopicId + '&returnUrl=/Topic/Topiclist.aspx" title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#pgtopic').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteTopic).end()
				.filter("a[rowId]").click(ShowTopic);

            $("#pgtopic").prev().find("td div").each(function () {
                if ($(this).html() == "已发布")
                    $(this).css("color", "#207D12");
                else if ($(this).html() == "未发布")
                    $(this).css("color", "#9F1416");
            });
        }
    });
}
//新增专题类型
function InsertTopicType(j_dialog) {
    if (ValidateFormType() == false) return;
    var topicTypeName = $('#txttypeTopicTypeName').val();
    var topicTypeGenerate = $('#drpTopicTypeGenerate option:selected').val();
    var templateId = $('#drptypeTemplateId option:selected').val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxTopic/InsertTopicType.cspx",
        type: "POST",
        data: { 'topicTypeName': topicTypeName, 'topicTypeGenerate': topicTypeGenerate, 'templateId': templateId},
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                window.location = window.location;
            });
        }
    });
}
//新增专题
 function InsertTopic(j_dialog) {
     var grid = $('#pgtopic');
     if (ValidateForm() == false) return;
     var topicName = $('#txtTopicName').val();
     var topicTypeId = $('#drpTopicTypeName option:selected').val();
     var remark = $('#txtRemark').val();
     var isTop = $("input[name='IsTop']:checked").val();
     var imgurl = $('#ImgUrl').val();
     var FileUrl = $('#FileUrl').val();
     var j_waitDialog = ShowWaitMessageDialog();
   
    $.ajax({
        url: "/AjaxTopic/InsertTopic.cspx",
        type: "POST",
        data: { 'topicName': topicName, 'topicTypeId': topicTypeId, 'remark': remark, 'isTop': isTop, 'imgurl': imgurl, 'FileUrl': FileUrl },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
               
                $("#divbody").css("display", "none");
                grid.datagrid('reload');
            });
        }
    });
}

//显示专题信息
function ShowTopic() {
    var dom = this;
    var topicId = $(this).attr("rowId");
    var grid = $('#pgtopic');
   
    // 首先获取指定的专题信息
    $.ajax({
        url: "/AjaxTopic/GetTopicById.cspx?topicId=" + topicId, dataType: "json",
        success: function (json) {
            $("#txtTopicName").val(json.TopicName);
            $("#drpTopicTypeName").val(json.TopicTypeId);
            $('#txtRemark').val(json.Remark);
            $('#CreatedTime').val(ConertJsonTimeAndFormat(json.CreatedTime, 'yyyy-MM-dd hh:mm:ss'));
            $('#topicId').val(topicId);
            $('#createdUser').val(json.CreatedUser);
            $('#Status').val(json.Status);
            if(json.PublishTime!=null)
                 $('#PublishTime').val(ConertJsonTimeAndFormat(json.PublishTime, 'yyyy-MM-dd hh:mm:ss'));
             //alert(json.ImgUrl)
            if (json.ImgUrl != null) {
                $("#imagesrc").html("<img src=" + json.ImgUrl + " width=60px height=80px/><input type='hidden' value=" + json.ImgUrl + " name='ImgUrl' id='ImgUrl'/>");
                $("#imagesrc").append("<span onclick='delImg()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
            } else {
                //$("#imagesrc").html("");
                $("#imagesrc>span").remove();
                $("#imagesrc>img").remove();
                $("#imagesrc>input").remove();
            }
            if (json.FileUrl != null) {
                $("#documentsrc").html("<input type='hidden' value=" + json.FileUrl + " name='FileUrl'/>");
                $("#documentsrc").append("<span onclick='delFile()' style='color:red;padding-left:10px;cursor:pointer;'>删除</span>");
            } else {
                $("#documentsrc>span").remove();
            }
            if (json.IsTop == 1) {
                $("#IsTopTrue").attr("checked", true);
                $("#IsTopfalse").attr("checked", false);
            }
            else {
                $("#IsTopTrue").attr("checked", false);
                $("#IsTopfalse").attr("checked", true);
            }
            $("#btn_save").css("display", "none");
            $("#btn_editsave").css("display", "");
            $("#divbody").css("display", "");
            // 显示编辑对话框
           // ShowEditItemDialog(topicId, 'divbody', 720, 400, function (j_dialog) {
                // 验证输入
               
            //});
        }
    });

    return false;
}

function editsave() {
    if (ValidateForm()) {
 
        var topicName = $('#txtTopicName').val();
        var topicTypeId = $('#drpTopicTypeName option:selected').val();
        var remark = $('#txtRemark').val();
        var createdUser = $('#createdUser').val();
        var status = $('#Status').val();
        var createdTime = $("#CreatedTime").val();
        var PublishTime = $("#PublishTime").val();
        var isTop = $("input[name='IsTop']:checked").val();
        var imgurl = $('#ImgUrl').val();
        var FileUrl = $('#FileUrl').val();
        var topicId = $("#topicId").val();
       
        $.ajax({
            type: "POST",
            url: "/AjaxTopic/updateTopic.cspx",
            data: { 'topicId': topicId, 'topicName': topicName, 'topicTypeId': topicTypeId, 'remark': remark, 'createdUser': createdUser, 'status': status, 'createdTime': createdTime, 'IsTop': isTop, 'imgurl': imgurl, 'FileUrl': FileUrl, 'PublishTime': PublishTime },
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (responseText) {
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                    
                    $("#divbody").css("display", "none");
                    grid.datagrid('reload');
                });
            }
        });
    }
}

//删除专题信息
function DelelteTopic() {
    var grid = $('#pgtopic');
    if (confirm('确定要删除此记录吗？')) {
        $.ajax({
            url: $(this).attr("linkurl"),
            success: function (responseText) {
                if (responseText > 0) {
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
    // 无论如何，都返回false
    return false;
}
//验证方法
function ValidateForm() {
    if (ValidateControl("#txtTopicName", "专题名称 不能为空。") == false) return false;
    if (ValidateControlValue("#drpTopicTypeName",0, "专题类型名称 不能为空。") == false) return false;
    return true;
}
function ValidateFormType() {
    if (ValidateControl("#txttypeTopicTypeName", "专题类型名称 不能为空。") == false) return false;
    if (ValidateControl("#drpTopicTypeGenerate", "专题生成类型 不能为空。") == false) return false; 
    if (ValidateControl("#drptypeTemplateId", "模板名称 不能为空。") == false) return false;  
    return true;
}
function ValidateControlValue(expression, value, message) {
    if ($.trim($(expression).val()) == value) {
        $.messager.alert(g_MsgBoxTitle, message, 'warning');
        return false;
    }
    return true;
}

//发布
function CreatePage(topicId, reDate) {
//    alert("嗯");

    $.ajax({
        url: "/AjaxTopic/CreatePage.cspx?topicId=" + topicId + "&reDate=" + reDate,
        type: "POST",
        success: function (res) {
            alert(res);
            if (res == "发布成功")
                FabuTopic(topicId);
        }
    });
}


//发布操作
function FabuTopic(topicId) {
    $.ajax({
        url: "/AjaxTopic/GetTopicFabuById.cspx?topicId=" + topicId,
        type: "POST",
        success: function (responseText) {
            //window.location = window.location;
            var grid = $('#pgtopic');
            grid.datagrid('reload');
        }
    });
}