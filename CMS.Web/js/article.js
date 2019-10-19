$(function () {

    //关闭弹窗推送编辑窗
    $("#closepic").click(function () {
        $("#PopupMsgInfo").css("display", "none");
        $('#PopupMsgPlanInfo').datagrid("reload");
    });

    self.parent.document.body.style.overflow = 'hidden';
    //分页列表
    $('#tblQueryResult').html('');
    BataSearchArticle();

    //查询
    $("#btnQuery").click(function () {
        $('#tblQueryResult').html('');
        BataSearchArticle();
    });


    //删除
    $("#Submit1").click(function () {
        if (confirm('你确定删除这些文章吗？')) {
            var pstr = $(".datagrid :input").fieldSerialize();
            if (pstr == "") {
                alert("你还没有选择要删除的文件!");
                return;
            }

            $.ajax({
                type: "GET",
                url: "/AjaxArticle/DelAllArticle.cspx?OperateType=BatchDel",
                data: pstr,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("删除成功");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //选择生成
    $("#Submit4").click(function () {

        if (confirm('你确定生成这些文章吗？')) {
            var pstr = $(".datagrid :input").fieldSerialize();
            if (pstr == "") {
                alert("你还没有选择要生成的文章!");
                return;
            }

            $.ajax({
                type: "GET",
                url: "/AjaxArticle/MakeArticle.cspx?OperateType=BatchMake",
                data: pstr,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("全部生成");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });
    //选择栏目生成
    $("#Submit6").click(function () {
         
        if (confirm('你确定生成这些文章吗？')) {
            var ChannelId = $("#ChannelId").val();
 
            $.ajax({
                type: "GET",
                url: "/AjaxArticle/MakeChannelIdArticle.cspx?ChannelId="+ChannelId,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("全部生成");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //一键生成
    $("#Submit5").click(function () {

        if (confirm('你确定生成这些文章吗？')) {
            $.ajax({
                type: "GET",
                url: "/AjaxArticle/MakeAllArticle.cspx?OperateType=BatchMake",
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("全部生成");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //一键生成
    $("#Submit7").click(function () {

        if (confirm('你确定生成这些文章吗？')) {
            $.ajax({
                type: "GET",
                url: "/AjaxArticle/MakeNoArticle.cspx?OperateType=NoBatchMake",
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("全部生成");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //批量公开
    $("#Submit2").click(function () {
        if (confirm('你确定公开这些文章吗？')) {
            var pstr = $(".datagrid :input").fieldSerialize();
            if (pstr == "") {
                alert("你还没有选择要删除的文件!");
                return;
            }
            $.ajax({
                type: "GET",
                url: "/AjaxArticle/AuditBatchArticle.cspx?OperateType=BatchAudit",
                data: pstr,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("操作成功");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //批量拒绝
    $("#Submit3").click(function () {
        if (confirm('你确定拒绝这些文章吗？')) {
            var pstr = $(".datagrid :input").fieldSerialize();
            if (pstr == "") {
                alert("你还没有选择要删除的文件!");
                return;
            }

            $.ajax({
                type: "GET",
                url: "/AjaxArticle/AuditBatchArticle.cspx?OperateType=BatchRefuse",
                data: pstr,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("操作成功");
                        $('#tblQueryResult').datagrid("reload");
                    }
                }
            });
        }
    });

    //更改权重
    $("#btnChange").click(function () {
        var newsId = $("#newsId").val();
        var orderNum = $("#orderNum").val();
        $.ajax({
            type: "GET",
            url: "/AjaxArticle/EidtOrderNum.cspx?OperateType=EditOrder",
            data: { "newsId": newsId, "orderNum": orderNum },
            success: function (responseText) {
                if (responseText == "000000") {
                    $("#SortingNum").css("display", "none");
                    alert("操作成功！");
                    $('#tblQueryResult').datagrid("reload");
                }
            }
        });

    })

    $("#btnAdd").click(function () {
        var check = CheckInput();
        if (check == false)
            return;
        var str = decodeURI($("#listtable :input").fieldSerialize());
        var SecondTitle = $("#hiddenTitle").val();
        var SecondUrl = $("#hiddentUrl").val();
        var TitleColor = $("#hiddenColor").val();
        var IsBold = $("#hiddenJC").val();
        var NewsId = $("#hiddenNewsId").val();
        var ChannelId = $("#hiddenChannelId").val();

        str = str + "&NewsId=" + NewsId + "&ChannelId=" + ChannelId + "&SecondTitle=" + SecondTitle + "&SecondUrl=" + SecondUrl + "&TitleColor=" + TitleColor + "&IsBold=" + IsBold;
        
        $.ajax({
            url: "/AjaxArticle/EditListNews.cspx",
            data: str,
            type: "POST",
            complete: function () { },
            success: function (responseText) {
                if (responseText == "000001") {
                    alert("标题不能为空");
                } else if (responseText == "000002") {
                    alert("分类有误");
                } else if (responseText == "000003") {
                    alert("请选择分类");
                } else {
                    HideAddTitles();
                    alert("修改成功");
                    $('#tblQueryResult').datagrid("reload");
                }
            }
        });
    })


});
 



//绑定模糊查询数据
function BataSearchArticle() {
    var dateRange = GetDateRange2("txtStartDate", "txtEndDate");
    var StartDate = dateRange.StartDate
    var EndDate = dateRange.EndDate
    var txtKeyword = $("#txtKeyword").val();
    var ChannelId = $("#ChannelId").val();

    $('#tblQueryResult').datagrid({
        title: "文件列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'NewsId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        type: "POST",
        url: '/AjaxArticle/SearchArticle.cspx?txtKeyword=' + encodeURIComponent(txtKeyword) + '&txtStartDate=' + StartDate + '&txtEndDate=' + EndDate + '&ChannelId=' + ChannelId,
        columns: [[
             { title: "<b>选择</b>", field: "NewsId", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     return value = '<input name="NewsId" id="NewsId" type="checkbox" value="' + row.NewsId + '" onclick="tes();" />';
                 }
             },

			 { title: "<b>标题</b>", field: "FullLink", align: 'left', width: fillSize(0.25),
			     formatter: function (value, row) {
			         var stitle = row.SecondTitle.replace("'","");
			         return value = row.FullLink + '&nbsp;&nbsp;<img src="/themes/default/images/admin/addtitle.gif" title="添加标题" style="cursor:pointer;" onclick="AddOtherTitles(' + row.NewsId + ',\'' + stitle + '\',\'' + row.SecondUrl + '\',\'' + row.IsBold + '\',\'' + row.TitleColor + '\',\'' + row.ChannelId + '\', this)" />';
			     }
			 },
			 { title: "<b>栏目</b>", field: "ChannelName", align: 'center', width: fillSize(0.1) },
             { title: "<b>类型</b>", field: "Sort", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     var typeName;
                     if (row.Sort == 1) {
                         typeName = "新文章";
                     }
                     else if (row.Sort == 2) {
                         typeName = "空文章";
                     } else if (row.Sort == 3) {
                         typeName = "图片文章";
                     } else {
                         typeName = "标题文章";
                     }
                     return value = typeName;
                 }
             },
             { title: "<b>责任编辑</b>", field: "CreatedUser", align: 'center', width: fillSize(0.05) },
             { title: "<b>权重</b>", field: "OrderNum", align: 'center', width: fillSize(0.05) },
              { title: "<b>点击数</b>", field: "ClickNum", align: 'center', width: fillSize(0.05) },
             { title: "<b>状态</b>", field: "Status", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     if (row.Status == 1) {
                         value = "已审核";
                     }
                     else if (row.Status == -1) {
                         value = "拒绝";
                     }
                     else {
                         value = "未审核";
                     }
                     return value;
                 }
             },
             { title: "<b>发布时间</b>", field: "CreatedTime", align: 'center', width: fillSize(0.10),
                 formatter: function (value, row) {

                     if (row.CreatedTime != "" && row.CreatedTime != null) {
                         var UploadDate = row.CreatedTime.replace("/Date(", "").replace(")/", "");
                         return value = getLocalTime(UploadDate.substr(0, 10));
                     } else {
                         return value = "";
                     }
                 }
             },
             { title: "<b>编辑</b>", field: "xx", align: 'left', width: fillSize(0.25),
                 formatter: function (value, row) {
                     var editurl;
                     var popup = "";
                     if (row.Sort == 1) {
                         editurl = "/news/editarticle.aspx?newsId=" + row.NewsId;
                         popup = '&nbsp;&nbsp;<a name="aa" style="display:none;" href="javascript:void(0)" rowId="' + row.NewsId + '"  onclick="showPopup(' + row.NewsId + ')" class="easyui-linkbutton" plain="true">推送</a>';
                     }
                     else if (row.Sort == 2) {
                         editurl = "/news/editemptyarticle.aspx?newsId=" + row.NewsId;
                     }
                     else if (row.Sort == 3) {
                         editurl = "/news/editpicarticle.aspx?newsId=" + row.NewsId;
                     } else {
                         editurl = "/news/edittitlearticle.aspx?newsId=" + row.NewsId;
                     }
                     return value = '<a href="javascript:void(0)" onclick="RefreshTime(' + row.NewsId + ')"  class="easyui-linkbutton" plain="true">' +
                               '刷时间</a>&nbsp;&nbsp;<a  href="' + editurl + '" class="easyui-linkbutton" rowId="' + row.NewsId + '" plain="true" target="_blank">' +
                               '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>' +
                               '&nbsp;&nbsp;<a href="javascript:void(0)" onclick="delSingleArticle(' + row.NewsId + ')"  class="easyui-linkbutton" plain="true">' +
                               '<img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>' +
                                '&nbsp;&nbsp;<a href="javascript:void(0)" onclick="auditArticle(' + row.NewsId + ',' + row.Status + ')"  class="easyui-linkbutton" plain="true">' +
                               '公开/拒绝</a>&nbsp;&nbsp;<a href="javascript:void(0)"  onclick="eidtOrderNum(' + row.NewsId + ',' + row.OrderNum + ', this)"   class="easyui-linkbutton" plain="true">' +
                               '改权重</a>' + popup;


                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			.filter(g_deleteButtonFilter).click(CommonDeleteRecord).end()

            loadAuthority();
        }
    });

}

function loadAuthority() {
    $.ajax({
        url: '/AjaxAuthorityDot/GetAuthorityDotAdmin.cspx?parentId=6',
        type: 'POST',
        success: function (json) {
            if (json[0].Text == "显示") {
                $("a[name='aa']").show();
            } else
                $("a[name='aa']").hide();
        }
    });
}

function showPopup(NewsId) {
    $("#PopupMsgInfo").css("display", "block");
    $("#changepopup").html("<iframe src='popupadd.aspx?PopupType=add&NewsId=" + NewsId + "&datatype=add' width='798px'  frameborder='0' height='100%'></iframe>");
}
 





function getPosition(el) {
    for (var lx = 0, ly = 0; el != null; lx += el.offsetLeft, ly += el.offsetTop, el = el.offsetParent);
    return { x: lx, y: ly }
}

 

function eidtOrderNum(newsId, orderNum, obj) {
    if ($("#SortingNum").css("display")=="") {
        $("#SortingNum").css("display", "none");
    }
    else {
        $("#SortingNum").css("display", "");
        $("#SortingNum").css("position", "absolute");
        $("#SortingNum").css("top",  getPosition(obj).y + obj.offsetHeight);
        $("#SortingNum").css("zIndex", "10000");
        $("#SortingNum").css("left", getPosition(obj).x-238);
        $("#newsId").val(newsId);
        $("#orderNum").val(orderNum);
        $("#orderNum").focus();
    }
}

function Hide() {
    $("#SortingNum").css("display", "none");
}	

//删除单篇文章
function delSingleArticle(newsId) {
    if (confirm('你确定删除这篇文章吗？')) {
            $.ajax({
                type: "GET",
                url: "/AjaxArticle/DelArticle.cspx?newsId=" + newsId + "&OperateType=SinggleDel",
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("删除成功！");
                        $('#tblQueryResult').datagrid("reload");
                    }    
                }
            });
        }    
}

//公开/拒绝单条文章
function auditArticle(newsId,status) {
    $.ajax({
        type: "GET",
        url: "/AjaxArticle/AuditArticle.cspx?OperateType=AuditOrRefuse&status=" + status,
        data: { "newsId": newsId },
        success: function (responseText) {
            if (responseText == "000000") {
                alert("操作成功！");
               $('#tblQueryResult').datagrid("reload");
            }
        }
    });
}

//刷时间
function RefreshTime(newsId) {
    $.ajax({
        type: "GET",
        url: "/AjaxArticle/RefreshTime.cspx?OperateType=RefreshTime",
        data: { "newsId": newsId },
        success: function (responseText) {
            if (responseText == "000000") {
                alert("操作成功！");
                $('#tblQueryResult').datagrid("reload");
            }
        }
    });
}

 