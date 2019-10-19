$(function () {
    //初始化数据
    self.parent.document.body.style.overflow = 'hidden';
    $('#pgTemp').html('');
    BataSearchPageTemplate();
    //添加
    $("#btnAddtemplate").click(function () {
        //重置
        $("#divbody").find("input:text").val("");
        $("#yes").attr("checked", "checked");
        $("#slctTempleteType").val(1);
        $("#txtRemark").val("");
        $("#txtHtmlPath").val("0");
        $("#txtOrderNum").val("0");
        $("#slctEncoding").val("UTF-8");
        ShowEditItemDialog('', 'divbody', 920, 350, function (j_dialog) { InsertPageTemplate(j_dialog); });
        return false;
    });
    //查询
    $("#btnSearch").click(function () {
        $('#pgTemp').html('');
        BataSearchPageTemplate();
    });
    $("table a.linkbutton[rowId]").click(ShowPageTemplate);
});

//绑定模糊查询数据
function BataSearchPageTemplate() {

    var fileName = $("#txtlikeFileName").val();
    var templateName = $("#txtlikeTemplateName").val();
    var templateType = $("#slctlikeTempleteType option:selected").val();

    $('#pgTemp').datagrid({
        title: "模板列表",
        height: getHeight(),
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'TemplateId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        pageList: [10, 30, 50], //可以设置每页记录条数的列表 
        showPageList: false,
        fitColumns: true,
        url: '/AjaxPageTemplate/SearchPageTemplate.cspx?templateType=' + templateType + '&fileName=' + encodeURIComponent(fileName) + '&templateName=' + encodeURIComponent(templateName),
        columns: [[
             { title: "<b>模板名称</b>", field: "TemplateName", width: fillSize(0.15) },
			 { title: "<b>模板文件路径</b>", field: "TemplateFileName", width: fillSize(0.2) },
              { title: "<b>模板生成文件路径</b>", field: "HtmlPath", width: fillSize(0.15) },
			 { title: "<b>创建人</b>", field: "CreatedUser", align: 'center', width: fillSize(0.05) },
              { title: "<b>模板编码</b>", field: "Encoding", align: 'center', width: fillSize(0.05) },
             { title: "<b>栏目属性</b>", field: "TempleteType", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     return value == 2 ? "<a rowtid='" + row.TemplateId + "'rowtype='" + row.TempleteType + "' href='javascript:void(0);'class='easyui-linkbutton' plain='true'>所属</a>" : "<a rowtid='" + row.TemplateId + "'rowtype='" + row.TempleteType + "' href='javascript:void(0);' class='easyui-linkbutton' plain='true'>涉及</a>";
                 }

             },
             { title: "<b>状态</b>", field: "Status", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     return value == true ? "开启" : "<label style='color:Red;'>停用</label>";
                 }
             },
              { title: "<b>预览</b>", field: "xx1", align: 'center', width: fillSize(0.05),
                  formatter: function (value, row) {
                      return row.TempleteType == 1 ? '<a href="../preview.aspx?TemplateId=' + row.TemplateId + '"  class="easyui-linkbutton" plain="true" target="_blank">预览</a>' : "原文";
                  }
              },
             { title: "<b>操作</b>", field: "xx2", align: 'center', width: fillSize(0.15),
                 formatter: function (value, row) {
                     var productButtonHtml = row.TempleteType == 1 ? '<a href="javascript:void(0);" itemTime="' + row.CreatedTime + '" class="easyui-linkbutton" itemId="' + row.TemplateId + '" plain="true">生成</a>/' : '';
                     return productButtonHtml + '<a href="javascript:void(0);" class="easyui-linkbutton" rowId="' + row.TemplateId + '" plain="true">编辑</a>&nbsp;&nbsp;';

                 }
             },
              { title: "<b>删除</b>", field: "xx3", align: 'center', width: fillSize(0.05),
                  formatter: function (value, row) {
                      return '<a  href="javascript:void(0);" class="easyui-linkbutton" linkurl="/AjaxPageTemplate/DeletePageTemplate.cspx?templateId=' + row.TemplateId + '" title="删除"  plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                  }
              }
		    ]],
        onLoadSuccess: function () {
            $($('#pgTemp').datagrid("getPanel")).find('a.easyui-linkbutton')
                 .filter(g_deleteButtonFilter).click(deleteTemplate).end()
                 .filter("a[rowtid]").click(windsopen).end()
                 .filter("a[rowId]").click(ShowPageTemplate).end()
                 .filter("a[itemId]").click(productTemplateHtml);
        }
    });
}



//删除模板记录
function deleteTemplate() {
    var grid=$('#pgTemp');
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

//打开新页面的方法
function windsopen() {
    var dom = this;
    var templateId = $(this).attr("rowtid");
    var templetetype = $(this).attr("rowtype");
    var WWidth = (window.screen.width - 500) / 2;
    var Wheight = (window.screen.height - 150) / 2;
    window.open('templatedetailmenu.aspx?templateId=' + templateId + '&templeteType=' + templetetype, '栏目列表选项', 'height=650px,width=500px,top=' + Wheight + ',left=' + WWidth + ',status=no');
}
//function MyshowModalDialog() {
//     var dom = this;
//    var templateId = $(this).attr("rowtid");
//    var templetetype = $(this).attr("rowtype");
//    window.showModalDialog('templatedetailmenu.aspx?templateId=' + templateId + '&templeteType=' + templetetype, 'dialogHeight:650px;dialogWidth:500px;status:no;');
// }
//添加方法
 function InsertPageTemplate(j_dialog) {
    if (ValidateForm() == false) return;
    var templateName = $("#txtTemplateName").val();
    var templateFileName = $("#txtTemplateFileName").val();
    var orderNum = $("#txtOrderNum").val();
    var htmlPath = $("#txtHtmlPath").val();
    var remark = $("#txtRemark").val();
    var status = $("input[type=radio][name=Status]:checked").val();
    var templeteType = $("#slctTempleteType option:selected").val();
    var encoding = $("#slctEncoding option:selected").val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: '/AjaxPageTemplate/InsertPageTemplate.cspx',
        data: { 'templateName': templateName,
                'templateFileName': templateFileName,
                'orderNum': orderNum,
                'htmlPath': htmlPath,
                'status': status,
                'remark': remark,
                'templeteType': templeteType,
                'encoding': encoding
            },
        type: 'POST',
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function () {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                j_dialog.dialog('close');
                $('#pgTemp').datagrid('reload');	            
            });   
         }
    })
}

function productTemplateHtml() {
    var templateId = $(this).attr("itemId");
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: '/AjaxPageTemplate/ProductListTemplateHtml.cspx?templateId=' + templateId,
        complete: function () { HideWaitMessageDialog(j_waitDialog); }       
    });
}



//数据显示的方法
function ShowPageTemplate() {
    var dom = this;
    var templateId = $(this).attr("rowId");
    $.ajax({
        url: '/AjaxPageTemplate/GetTemplateBytemplateById.cspx?templateId=' + templateId,
        dataType: "json",
        success: function (json) {
            $("#txtTemplateName").val(json.TemplateName);
            $("#txtTemplateFileName").val(json.TemplateFileName);
            $("#txtOrderNum").val(json.OrderNum);
            $("#txtHtmlPath").val(json.HtmlPath);
            $("#txtRemark").val(json.Remark);
            $("#slctTempleteType").val(json.TempleteType);
            $("#hdCreatedUser").val(json.CreatedUser);
            var jsonTime = ConertJsonTimeAndFormat(json.CreatedTime, 'yyyy-MM-dd hh:mm:ss');
            $("#hdCreatedTime").val(jsonTime);
            if (json.Status == true) {
                $("#no").attr("checked", "");
                $("#yes").attr("checked", "checked");
            } else {
                $("#yes").attr("checked", "");
                $("#no").attr("checked", "checked");
            }
            $("#slctEncoding").val(json.Encoding);
            // 显示编辑对话框
            ShowEditItemDialog(templateId, 'divbody', 920, 350, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var templateName = $("#txtTemplateName").val();
                    var templateFileName = $("#txtTemplateFileName").val();
                    var orderNum = $("#txtOrderNum").val();
                    var htmlPath = $("#txtHtmlPath").val();
                    var remark = $("#txtRemark").val();
                    var templeteType = $("#slctTempleteType option:selected").val();
                    var status = $("input[type=radio][name=Status]:checked").val();
                    var encoding = $("#slctEncoding option:selected").val();
                    var createdUser = $("#hdCreatedUser").val();
                    var createdTime = $("#hdCreatedTime").val();
                    $.ajax({
                        type: "POST",
                        url: "/AjaxPageTemplate/UpdatePageTemplate.cspx",
                        data: { 'templateId': templateId,
                            'templateName': templateName,
                            'templateFileName': templateFileName,
                            'orderNum': orderNum,
                            'htmlPath': htmlPath,
                            'remark': remark,
                            'templeteType': templeteType,
                            'status': status,
                            'encoding': encoding,
                            'createdUser': createdUser,
                            'createdTime': createdTime
                        },
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function (responseText) {
                            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                                j_dialog.hide().dialog('close');
                                $('#pgTemp').datagrid('reload');
                            });
                        }
                    });
                }
            });
        }
    });
}

//验证方法
function ValidateForm() {
    if (ValidateControl("#txtTemplateName", "模板名称 不能为空。") == false) return false;
    if (ValidateControl("#txtTemplateFileName", "文件名 不能为空。") == false) return false;
    if (ValidateControl("#txtOrderNum", "排序值 不能为空。") == false) return false;
    if (ValidateControl("#txtHtmlPath", "目录路径 不能为空。") == false) return false;
    return true;
}
